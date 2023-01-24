using System.Diagnostics;
using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Exceptions;
using Elixus.Discord.Gateway.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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
	private readonly ClientWebSocket _socket = new();
	private readonly Memory<byte> _buffer = new(new byte[4096]);

	public DefaultDiscordGateway(ILogger<DefaultDiscordGateway> logger, IServiceScopeFactory scopeFactory)
	{
		_logger = logger;
		_scopeFactory = scopeFactory;
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

	public async Task SendAsync(ArraySegment<byte> payload, CancellationToken cancellationToken)
	{
		await _socket.SendAsync(payload, WebSocketMessageType.Text, true, cancellationToken);
	}

	/// <summary>
	/// Parses the <paramref name="received" /> payload and dispatches it to the required handlers within the given <paramref name="scope" />.
	/// </summary>
	private ValueTask ParseAndDispatchPayload(IServiceScope scope, ReadOnlySpan<byte> received, out int? sequence, CancellationToken cancellationToken)
	{
		var context = GatewayEventSerializerContext.Default;
		var payload = new EventPayload();
		ParseEventPayload(ref payload, received);
		sequence = payload.Sequence;

		_logger.LogTrace("Received gateway event with Opcode {Opcode} and EventName {EventName}", payload.Opcode, payload.EventName);
		return payload.Opcode switch
		{
			GatewayOpcodes.Hello => DispatchPayload(scope, ref payload, context.HelloEvent, cancellationToken),
			GatewayOpcodes.Heartbeat => DispatchPayload(scope, ref payload, context.HeartbeatEvent, cancellationToken),
			GatewayOpcodes.Reconnect => DispatchPayload(scope, ref payload, context.ReconnectEvent, cancellationToken),
			GatewayOpcodes.InvalidSession => DispatchPayload(scope, ref payload, context.InvalidSessionEvent, cancellationToken),
			GatewayOpcodes.HeartbeatAck => DispatchPayload(scope, ref payload, context.HeartbeatAckEvent, cancellationToken),
			// Opcodes below are send-only and cannot be received, but intellisense prefers all arms to be present.
			GatewayOpcodes.Identify
			or GatewayOpcodes.PresenceUpdate
			or GatewayOpcodes.VoiceStateUpdate
			or GatewayOpcodes.Resume
			or GatewayOpcodes.RequestGuildMembers
			or _ => throw new ArgumentOutOfRangeException(nameof(payload.Opcode), payload.Opcode, "Don't know how to handle opcode")
		};
	}

	/// <summary>
	/// Parses the received binary payload
	/// </summary>
	private void ParseEventPayload(ref EventPayload payload, ReadOnlySpan<byte> received)
	{
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
					payload.Opcode = (GatewayOpcodes)reader.GetInt32();
					break;
				case "s":
					payload.Sequence = reader.TokenType is JsonTokenType.Null ? null : reader.GetInt32();
					break;
				case "t":
					payload.EventName = reader.TokenType is JsonTokenType.Null ? null : reader.GetString();
					break;
				case "d" when reader.TokenType is JsonTokenType.StartObject:
				{
					var start = (int)reader.TokenStartIndex;
					reader.Skip();
					var end = (int)reader.BytesConsumed;
					payload.EventData = received[start..end];
					return;
				}
				case "d" when reader.TokenType is JsonTokenType.Null:
					payload.EventData = ReadOnlySpan<byte>.Empty;
					return;
				default:
					throw new GatewayParserException(JsonTokenType.None, reader.TokenType, reader.BytesConsumed);
			}
		}

		// If we get here we missed the event payload (which returns), so we should throw.
		throw new GatewayParserException(JsonTokenType.None, reader.TokenType, reader.BytesConsumed);
	}

	/// <summary>
	/// Small helper function to deserialize the event and dispatch to the registered <see cref="IEventHandler{TEvent}" />.
	/// </summary>
	private ValueTask DispatchPayload<TEvent>(IServiceScope scope, ref EventPayload payload, JsonTypeInfo<TEvent> typeInfo, CancellationToken cancellationToken) where TEvent : GatewayEvent, new()
	{
		var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();
		var @event = payload.EventData.IsEmpty switch
		{
			true => new TEvent(),
			_ => JsonSerializer.Deserialize(payload.EventData, typeInfo)
		};

		;
		if (@event is null)
			throw new InvalidOperationException($"Failed to deserialize event of type {typeof(TEvent).FullName}");

		return handler.HandleEvent(@event, cancellationToken);
	}
}
