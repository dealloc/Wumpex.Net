using System.Diagnostics;
using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text.Json;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway;

/// <summary>
/// The default implementation of the <see cref="IDiscordGateway" /> using System.Net.WebSockets
/// </summary>
/// <seealso cref="IDiscordGateway" />
internal sealed class DefaultDiscordGateway : IDiscordGateway, IDisposable
{
	private readonly ILogger<DefaultDiscordGateway> _logger;
	private readonly IHeartbeatService _heartbeatService;
	private readonly IEventSerializer<HelloEvent> _helloSerializer;
	private readonly IEventHandler<HelloEvent> _helloHandler;
	private readonly IEventSerializer<HeartbeatEvent> _heartbeatSerializer;
	private readonly IEventHandler<HeartbeatEvent> _heartbeatHandler;
	private readonly IEventSerializer<ReconnectEvent> _reconnectSerializer;
	private readonly IEventHandler<ReconnectEvent> _reconnectHandler;
	private readonly IEventSerializer<InvalidSessionEvent> _invalidSessionSerializer;
	private readonly IEventHandler<InvalidSessionEvent> _invalidSessionHandler;
	private readonly IEventSerializer<HeartbeatAckEvent> _heartbeatAckSerializer;
	private readonly IEventHandler<HeartbeatAckEvent> _heartbeatAckHandler;
	private readonly IEventSerializer<IdentifyEvent> _identifySerializer;
	private readonly IDispatchEventHandler _dispatchEventHandler;

	private readonly ClientWebSocket _socket = new();
	private readonly Memory<byte> _buffer = new(new byte[4096]);

	/// <remarks>
	/// Including a serializer and a handler directly instead of resolving from the container at runtime has two purposes:
	/// - invalid service registration will be immediately thrown when starting the application (or at least first resolving IDiscordGateway)
	/// - only resolves once and allows for a type-safe dispatch, even if it means slightly more boilerplate code.
	/// </remarks>
	public DefaultDiscordGateway(ILogger<DefaultDiscordGateway> logger,
		IHeartbeatService heartbeatService,
		IEventSerializer<HelloEvent> helloSerializer,
		IEventHandler<HelloEvent> helloHandler,
		IEventSerializer<HeartbeatEvent> heartbeatSerializer,
		IEventHandler<HeartbeatEvent> heartbeatHandler,
		IEventSerializer<ReconnectEvent> reconnectSerializer,
		IEventHandler<ReconnectEvent> reconnectHandler,
		IEventSerializer<InvalidSessionEvent> invalidSessionSerializer,
		IEventHandler<InvalidSessionEvent> invalidSessionHandler,
		IEventSerializer<HeartbeatAckEvent> heartbeatAckSerializer,
		IEventHandler<HeartbeatAckEvent> heartbeatAckHandler,
		IEventSerializer<IdentifyEvent> identifySerializer,
		IDispatchEventHandler dispatchEventHandler)
	{
		_logger = logger;
		_heartbeatService = heartbeatService;
		_helloSerializer = helloSerializer;
		_helloHandler = helloHandler;
		_heartbeatSerializer = heartbeatSerializer;
		_heartbeatHandler = heartbeatHandler;
		_reconnectSerializer = reconnectSerializer;
		_reconnectHandler = reconnectHandler;
		_invalidSessionSerializer = invalidSessionSerializer;
		_invalidSessionHandler = invalidSessionHandler;
		_heartbeatAckSerializer = heartbeatAckSerializer;
		_heartbeatAckHandler = heartbeatAckHandler;
		_identifySerializer = identifySerializer;
		_dispatchEventHandler = dispatchEventHandler;
	}

	/// <inheritdoc cref="IDisposable.Dispose"/>
	public void Dispose() => _socket.Dispose();

	/// <inheritdoc cref="IDiscordGateway.RunAsync"/>
	public async Task RunAsync(Uri endpoint, CancellationToken cancellationToken = default)
	{
		var performance = new Stopwatch();
		await _socket.ConnectAsync(endpoint, cancellationToken);

		while (_socket.State is WebSocketState.Open && cancellationToken.IsCancellationRequested is false)
		{
			var result = await _socket.ReceiveAsync(_buffer, cancellationToken);

			if (result.MessageType is WebSocketMessageType.Close)
				break;
			if (result.EndOfMessage is false)
				throw new NotSupportedException("Elixus.Discord currently does not support messages larger than 4096 bytes");

			performance.Restart();
			await ParseAndDispatchPayload(_buffer.Span[..result.Count], out var sequence, cancellationToken);
			await _heartbeatService.Notify(sequence, cancellationToken);
			_logger.LogTrace("Finished handling {EventSequence} in {Elapsed}", sequence, performance.Elapsed);
		}

		// TODO: check close code if we should reconnect?
		_logger.LogInformation("Discord gateway shutting down with status {StatusCode} ('{StatusMessage}')", _socket.CloseStatus, _socket.CloseStatusDescription ?? "Unspecified");
	}

	/// <inheritdoc cref="IDiscordGateway.SendAsync{TEvent}" />
	public Task SendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, new()
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
		lock (_socket)
		{
			return _socket.SendAsync(payload, WebSocketMessageType.Text, true, cancellationToken);
		}
	}

	/// <summary>
	/// Parses the <paramref name="received" /> payload and dispatches it to the required Serializers and Handlers.
	/// </summary>
	private ValueTask ParseAndDispatchPayload(ReadOnlySpan<byte> received, out int? sequence, CancellationToken cancellationToken)
	{
		var context = ParseEventPayload(received, out var payload);
		sequence = context.Sequence;

		_logger.LogTrace("Received gateway event with Opcode {Opcode} and EventName {EventName}", context.Opcode, context.EventName);
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
	private ValueTask DispatchPayload<TEvent>(EventContext context, ref ReadOnlySpan<byte> payload, IEventSerializer<TEvent> serializer, IEventHandler<TEvent> handler, CancellationToken cancellationToken) where TEvent : class, new()
	{
		var @event = serializer.Deserialize(payload);

		return handler.HandleEvent(@event, context, cancellationToken);
	}
}
