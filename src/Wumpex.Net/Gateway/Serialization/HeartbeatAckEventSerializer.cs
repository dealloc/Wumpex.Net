using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;

namespace Wumpex.Net.Gateway.Serialization;

internal class HeartbeatAckEventSerializer : IEventSerializer<HeartbeatAckEvent>
{
	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(HeartbeatAckEvent @event)
		=> Array.Empty<byte>();

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public HeartbeatAckEvent Deserialize(ReadOnlySpan<byte> payload)
		=> new();
}