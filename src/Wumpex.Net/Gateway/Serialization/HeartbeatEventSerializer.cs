using System.Text.Json;
using Wumpex.Net.Gateway.Constants;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;

namespace Wumpex.Net.Gateway.Serialization;

internal class HeartbeatEventSerializer : IEventSerializer<HeartbeatEvent>
{
	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	/// <remarks>
	/// The payload to send is a primitive (int) so we can't use the event directly.
	/// Instead, we just the <see cref="Utf8JsonWriter" /> directly to write the event data.
	/// Bonus points, it's pretty fast too
	/// </remarks>
	public ArraySegment<byte> Serialize(HeartbeatEvent @event)
	{
		using var memory = new MemoryStream();
		var writer = new Utf8JsonWriter(memory);
		writer.WriteStartObject(); // {
		writer.WriteNumber("op", (int)GatewayOpcodes.Heartbeat);
		if (@event.Sequence is { } value)
			writer.WriteNumber("d", value);
		else
			writer.WriteNull("d");
		writer.WriteEndObject(); // }

		writer.Flush();
		return memory.ToArray();
	}

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public HeartbeatEvent Deserialize(ReadOnlySpan<byte> payload)
		=> new();
}