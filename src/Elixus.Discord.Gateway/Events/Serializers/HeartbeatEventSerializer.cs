using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Parsing;

namespace Elixus.Discord.Gateway.Events.Serializers;

internal class HeartbeatEventSerializer : IEventSerializer<HeartbeatEvent>
{
	private readonly JsonTypeInfo<HeartbeatEvent> _typeInfo = GatewayEventSerializerContext.Default.HeartbeatEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(HeartbeatEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event, _typeInfo);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public HeartbeatEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;
}