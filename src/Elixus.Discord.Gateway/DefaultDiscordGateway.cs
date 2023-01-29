using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;
using Elixus.Discord.Gateway.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Channels;
using Elixus.Discord.Core.Exceptions;

namespace Elixus.Discord.Gateway;

/// <summary>
/// The default implementation of the <see cref="IDiscordGateway" /> using System.Net.WebSockets
/// </summary>
/// <seealso cref="IDiscordGateway" />
internal sealed class DefaultDiscordGateway : IDiscordGateway, IDisposable
{
	/// <inheritdoc cref="IDiscordGateway.CanRecover" />
	/// <remarks>We await <see cref="ResumeEndpoint" /> and <see cref="ResumeSession" /> since we know internally they're synchronously set.</remarks>
	public ValueTask<bool> CanRecover => ValueTask.FromResult(_resumeEndpoint is not null && string.IsNullOrWhiteSpace(_resumeSession) is false);

	/// <inheritdoc cref="IDiscordGateway.ResumeEndpoint" />
	public ValueTask<Uri?> ResumeEndpoint => ValueTask.FromResult(_resumeEndpoint);

	/// <inheritdoc cref="IDiscordGateway.ResumeSession" />
	public ValueTask<string?> ResumeSession => ValueTask.FromResult(_resumeSession);

	private readonly ILogger<DefaultDiscordGateway> _logger;
	private readonly Lazy<IHeartbeatService> _heartbeatService;
	private readonly IEventSerializer<HelloEvent> _helloSerializer;
	private readonly Lazy<IEventHandler<HelloEvent>> _helloHandler;
	private readonly IEventSerializer<HeartbeatEvent> _heartbeatSerializer;
	private readonly Lazy<IEventHandler<HeartbeatEvent>> _heartbeatHandler;
	private readonly IEventSerializer<ReconnectEvent> _reconnectSerializer;
	private readonly Lazy<IEventHandler<ReconnectEvent>> _reconnectHandler;
	private readonly IEventSerializer<InvalidSessionEvent> _invalidSessionSerializer;
	private readonly Lazy<IEventHandler<InvalidSessionEvent>> _invalidSessionHandler;
	private readonly IEventSerializer<HeartbeatAckEvent> _heartbeatAckSerializer;
	private readonly Lazy<IEventHandler<HeartbeatAckEvent>> _heartbeatAckHandler;
	private readonly IEventSerializer<IdentifyEvent> _identifySerializer;
	private readonly IDispatchEventHandler _dispatchEventHandler;

	private Uri? _resumeEndpoint;
	private string? _resumeSession;
	private readonly ClientWebSocket _socket = new();
	private readonly Memory<byte> _buffer = new(new byte[102400]);
	private readonly Channel<byte[]> _events = Channel.CreateBounded<byte[]>(new BoundedChannelOptions(1)
	{
		SingleWriter = true,
		FullMode = BoundedChannelFullMode.Wait
	});


	/// <remarks>
	/// Including a serializer and a handler directly instead of resolving from the container at runtime has two purposes:
	/// - invalid service registration will be immediately thrown when starting the application (or at least first resolving IDiscordGateway)
	/// - only resolves once and allows for a type-safe dispatch, even if it means slightly more boilerplate code.
	/// </remarks>
	public DefaultDiscordGateway(ILogger<DefaultDiscordGateway> logger,
		IServiceProvider serviceProvider,
		IEventSerializer<HelloEvent> helloSerializer,
		IEventSerializer<HeartbeatEvent> heartbeatSerializer,
		IEventSerializer<ReconnectEvent> reconnectSerializer,
		IEventSerializer<InvalidSessionEvent> invalidSessionSerializer,
		IEventSerializer<HeartbeatAckEvent> heartbeatAckSerializer,
		IEventSerializer<IdentifyEvent> identifySerializer,
		IDispatchEventHandler dispatchEventHandler)
	{
		_logger = logger;
		_helloSerializer = helloSerializer;
		_heartbeatSerializer = heartbeatSerializer;
		_reconnectSerializer = reconnectSerializer;
		_invalidSessionSerializer = invalidSessionSerializer;
		_heartbeatAckSerializer = heartbeatAckSerializer;
		_identifySerializer = identifySerializer;
		_dispatchEventHandler = dispatchEventHandler;

		// some services might refer to the gateway, so if we directly inject them we'll create a circular dependency
		// Instead we wrap them in Lazy<T> so they're resolved when required for the first time from the root scope.
		_heartbeatService = new(serviceProvider.GetRequiredService<IHeartbeatService>);
		_helloHandler = new(serviceProvider.GetRequiredService<IEventHandler<HelloEvent>>);
		_heartbeatHandler = new(serviceProvider.GetRequiredService<IEventHandler<HeartbeatEvent>>);
		_reconnectHandler = new(serviceProvider.GetRequiredService<IEventHandler<ReconnectEvent>>);
		_invalidSessionHandler = new(serviceProvider.GetRequiredService<IEventHandler<InvalidSessionEvent>>);
		_heartbeatAckHandler = new(serviceProvider.GetRequiredService<IEventHandler<HeartbeatAckEvent>>);
	}

	/// <inheritdoc cref="IDisposable.Dispose"/>
	public void Dispose() => _socket.Dispose();

	/// <inheritdoc cref="IDiscordGateway.RunAsync"/>
	public async Task RunAsync(Uri endpoint, CancellationToken cancellationToken = default)
	{
		await _socket.ConnectAsync(endpoint, cancellationToken);
		var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

			// we use WhenAny since WhenAll does not throw when a task fails.
			// WhenAny will return the failed task, which we await to propagate the error.
			// *IF* a task were to actually close normally we cancel `linked` to close all other listeners as well.
		var result = await Task.WhenAny(
			ListenForIncomingEvents(linked.Token),
			ListenForOutgoingEvents(linked.Token)
		);

		linked.Cancel(); // ensures all listeners will be closed.
		await result;
	}

	/// <summary>
	/// Runs the internal listener loop for reading incoming events.
	/// </summary>
	private async Task ListenForIncomingEvents(CancellationToken cancellationToken)
	{
		var performance = new Stopwatch();
		while (_socket.State is WebSocketState.Open && cancellationToken.IsCancellationRequested is false)
		{
			try
			{
				var result = await _socket.ReceiveAsync(_buffer, cancellationToken);

				if (result.MessageType is WebSocketMessageType.Close)
					throw new GatewayClosedException(_socket);
				if (result.EndOfMessage is false)
					throw new NotSupportedException($"Elixus.Discord currently does not support messages larger than {_buffer.Length} bytes");

				performance.Restart();
				await ParseAndDispatchPayload(_buffer.Span[..result.Count], out var sequence, cancellationToken);
				await _heartbeatService.Value.Notify(sequence, cancellationToken);
				_logger.LogTrace("Finished handling {EventSequence} in {Elapsed}", sequence, performance.Elapsed);
			}
			catch (GatewayClosedException exception) when (exception.CanRecover is not true)
			{
				_logger.LogCritical(exception, "Discord Gateway closed and cannot recover from status {StatusCode} ('{StatusMessage}')", exception.CloseCode, exception.Message);
				_resumeEndpoint = null;
				_resumeSession = null;

				throw;
			}
			catch (Exception exception)
			{
				_logger.LogCritical(exception, "An error occured on the Discord WS Gateway");
				if (exception is DiscordException { CanRecover: not true })
				{
					_resumeEndpoint = null;
					_resumeSession = null;
				}

				throw;
			}
		}
	}

	/// <summary>
	/// Starts the internal listener for outgoing events to send out.
	/// </summary>
	/// <remarks>
	/// The <see cref="ClientWebSocket" /> is not thread-safe for writing from multiple locations.
	/// The channel provides a synchronized way of writing events away without locking.
	/// If we'd just `lock`, the compiler would (rightfully) complain that we're not always synchronizing access to _socket.
	/// </remarks>
	private async Task ListenForOutgoingEvents(CancellationToken cancellationToken)
	{
		await foreach (var @event in _events.Reader.ReadAllAsync(cancellationToken))
		{
			await _socket.SendAsync(@event, WebSocketMessageType.Text, true, cancellationToken);
		}
	}

	/// <inheritdoc cref="IDiscordGateway.SendAsync{TEvent}" />
	public async Task SendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, new()
	{
		var payload = @event switch
		{
			HelloEvent hello => _helloSerializer.Serialize(hello),
			HeartbeatEvent heartbeat => _heartbeatSerializer.Serialize(heartbeat),
			ReconnectEvent reconnect => _reconnectSerializer.Serialize(reconnect),
			InvalidSessionEvent invalid => _invalidSessionSerializer.Serialize(invalid),
			HeartbeatAckEvent heartbeat => _heartbeatAckSerializer.Serialize(heartbeat),
			IdentifyEvent identify => _identifySerializer.Serialize(identify),
			_ => throw new NotSupportedException($"Cannot send {@event.GetType().FullName} over gateway, no known serializer")
		};

		// ClientWebSocket is not thread-safe for sending, so we lock here to ensure safe access.
		// we copy the binary payload (with `ToArray()`) to ensure it's memory is dedicated for this message and won't be GC'd.
		await _events.Writer.WriteAsync(payload.ToArray(), cancellationToken);
	}

	/// <inheritdoc cref="IDiscordGateway.ConfigureReconnect" />
	public ValueTask ConfigureReconnect(Uri endpoint, string session, CancellationToken cancellationToken = default)
	{
		_resumeEndpoint = endpoint;
		_resumeSession = session;

		return ValueTask.CompletedTask;
	}

	/// <summary>
	/// Parses the <paramref name="received" /> payload and dispatches it to the required Serializers and Handlers.
	/// </summary>
	private ValueTask ParseAndDispatchPayload(ReadOnlySpan<byte> received, out int? sequence, CancellationToken cancellationToken)
	{
		var context = ParseEventPayload(received, out var payload);
		sequence = context.Sequence;

		_logger.LogTrace("Received gateway event with Opcode {Opcode} ('{EventName}') as {Sequence}", context.Opcode, context.EventName, context.Sequence);
		return context.Opcode switch
		{
			GatewayOpcodes.Hello => DispatchPayload(context, ref payload, _helloSerializer, _helloHandler, cancellationToken),
			GatewayOpcodes.Heartbeat => DispatchPayload(context, ref payload, _heartbeatSerializer, _heartbeatHandler, cancellationToken),
			GatewayOpcodes.Reconnect => DispatchPayload(context, ref payload, _reconnectSerializer, _reconnectHandler, cancellationToken),
			GatewayOpcodes.InvalidSession => DispatchPayload(context, ref payload, _invalidSessionSerializer, _invalidSessionHandler, cancellationToken),
			GatewayOpcodes.HeartbeatAck => DispatchPayload(context, ref payload, _heartbeatAckSerializer, _heartbeatAckHandler, cancellationToken),
			GatewayOpcodes.Dispatch => _dispatchEventHandler.HandleDispatch(context, ref payload, cancellationToken),
			// Opcodes below are send-only and cannot be received, but intellisense prefers all arms to be present.
			GatewayOpcodes.Identify
			or GatewayOpcodes.PresenceUpdate
			or GatewayOpcodes.VoiceStateUpdate
			or GatewayOpcodes.Resume
			or GatewayOpcodes.RequestGuildMembers
			or _ => throw new ArgumentOutOfRangeException(nameof(context.Opcode), context.Opcode, "Don't know how to handle opcode")
		};
	}

	/// <summary>
	/// Extracts the <see cref="EventContext" /> information from the request along with the event payload.
	/// </summary>
	/// <param name="received">The binary data received from the socket.</param>
	/// <param name="event">The actual event payload, exposed as an out param.</param>
	/// <see href="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
	private EventContext ParseEventPayload(ReadOnlySpan<byte> received, out ReadOnlySpan<byte> @event)
	{
		var context = new EventContext();
		var reader = new Utf8JsonReader(received);
		reader.Read(); // advance to first symbol.

		while (reader.Read())
		{
			if (reader.TokenType is not JsonTokenType.PropertyName)
				throw new GatewayParserException(JsonTokenType.PropertyName, reader.TokenType, reader.BytesConsumed);

			var propertyName = reader.GetString()!;
			reader.Read();

			switch (propertyName)
			{
				case "op":
					context.Opcode = (GatewayOpcodes)reader.GetInt32();
					break;
				case "s":
					context.Sequence = reader.TokenType is JsonTokenType.Null ? null : reader.GetInt32();
					break;
				case "t":
					context.EventName = reader.TokenType is JsonTokenType.Null ? null : reader.GetString();
					break;
				case "d" when reader.TokenType is JsonTokenType.StartObject:
				{
					var start = (int)reader.TokenStartIndex;
					reader.Skip();
					var end = (int)reader.BytesConsumed;
					@event = received[start..end];
					return context;
				}
				case "d" when reader.TokenType is JsonTokenType.Null:
					@event = ReadOnlySpan<byte>.Empty;
					return context;
				default:
					throw new GatewayParserException(JsonTokenType.None, reader.TokenType, reader.BytesConsumed);
			}
		}

		// If we get here we missed the event payload (which returns), so we should throw.
		throw new GatewayParserException(JsonTokenType.None, reader.TokenType, reader.BytesConsumed);
	}

	/// <summary>
	/// Small helper function to deserialize the event and dispatch to the registered <see cref="IEventSerializer{TEvent}" />.
	/// </summary>
	private ValueTask DispatchPayload<TEvent>(EventContext context, ref ReadOnlySpan<byte> payload, IEventSerializer<TEvent> serializer, Lazy<IEventHandler<TEvent>> handler, CancellationToken cancellationToken) where TEvent : class, new()
	{
		var @event = serializer.Deserialize(payload);

		return handler.Value.HandleEvent(@event, context, cancellationToken);
	}
}
