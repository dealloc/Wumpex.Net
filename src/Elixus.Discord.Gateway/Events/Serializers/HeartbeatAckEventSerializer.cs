using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Events.Serializers;

internal class HeartbeatAckEventSerializer : IEventSerializer<HeartbeatAckEvent>
{
	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(HeartbeatAckEvent @event)
		=> Array.Empty<byte>();

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public HeartbeatAckEvent Deserialize(ReadOnlySpan<byte> payload)
		=> new();
}