using System.Diagnostics;
using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Exceptions;
using Microsoft.Extensions.DependencyInjection;
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
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly IEventSerializer<HelloEvent> _helloSerializer;
	private readonly IEventSerializer<HeartbeatEvent> _heartbeatSerializer;
	private readonly IEventSerializer<ReconnectEvent> _reconnectSerializer;
	private readonly IEventSerializer<InvalidSessionEvent> _invalidSessionSerializer;
	private readonly IEventSerializer<HeartbeatAckEvent> _heartbeatAckSerializer;
	private readonly IEventSerializer<IdentifyEvent> _identifySerializer;
	private readonly ClientWebSocket _socket = new();
	private readonly Memory<byte> _buffer = new(new byte[4096]);

	public DefaultDiscordGateway(ILogger<DefaultDiscordGateway> logger,
		IServiceScopeFactory scopeFactory,
		IEventSerializer<HelloEvent> helloSerializer,
		IEventSerializer<HeartbeatEvent> heartbeatSerializer,
		IEventSerializer<ReconnectEvent> reconnectSerializer,
		IEventSerializer<InvalidSessionEvent> invalidSessionSerializer,
		IEventSerializer<HeartbeatAckEvent> heartbeatAckSerializer,
		IEventSerializer<IdentifyEvent> identifySerializer)
	{
		_logger = logger;
		_scopeFactory = scopeFactory;
		_helloSerializer = helloSerializer;
		_heartbeatSerializer = heartbeatSerializer;
		_reconnectSerializer = reconnectSerializer;
		_invalidSessionSerializer = invalidSessionSerializer;
		_heartbeatAckSerializer = heartbeatAckSerializer;
		_identifySerializer = identifySerializer;
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

			await using var scope = _scopeFactory.CreateAsyncScope();

			performance.Restart();
			await ParseAndDispatchPayload(scope, _buffer.Span[..result.Count], out var sequence, cancellationToken);
			await scope.ServiceProvider.GetRequiredService<IHeartbeatService>().Notify(sequence, cancellationToken);
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
	/// Parses the <paramref name="received" /> payload and dispatches it to the required Serializers within the given <paramref name="scope" />.
	/// </summary>
	private ValueTask ParseAndDispatchPayload(IServiceScope scope, ReadOnlySpan<byte> received, out int? sequence, CancellationToken cancellationToken)
	{
		var context = ParseEventPayload(received, out var payload);
		sequence = context.Sequence;

		_logger.LogTrace("Received gateway event with Opcode {Opcode} and EventName {EventName}", context.Opcode, context.EventName);
		return context.Opcode switch
		{
			GatewayOpcodes.Hello => DispatchPayload(scope, context, ref payload, _helloSerializer, cancellationToken),
			GatewayOpcodes.Heartbeat => DispatchPayload(scope, context, ref payload, _heartbeatSerializer, cancellationToken),
			GatewayOpcodes.Reconnect => DispatchPayload(scope, context, ref payload, _reconnectSerializer, cancellationToken),
			GatewayOpcodes.InvalidSession => DispatchPayload(scope, context, ref payload, _invalidSessionSerializer, cancellationToken),
			GatewayOpcodes.HeartbeatAck => DispatchPayload(scope, context, ref payload, _heartbeatAckSerializer, cancellationToken),
			// Opcodes below are send-only and cannot be received, but intellisense prefers all arms to be present.
			GatewayOpcodes.Dispatch
			or GatewayOpcodes.Identify
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
	/// <see cref="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
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
	private ValueTask DispatchPayload<TEvent>(IServiceScope scope, EventContext context, ref ReadOnlySpan<byte> payload, IEventSerializer<TEvent> serializer, CancellationToken cancellationToken) where TEvent : class, new()
	{
		var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();

		var @event = serializer.Deserialize(payload);
		return handler.HandleEvent(@event, context, cancellationToken);
	}
}
