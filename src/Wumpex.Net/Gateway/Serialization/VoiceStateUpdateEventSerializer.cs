using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Wumpex.Net.Core.Events.Voice;
using Wumpex.Net.Gateway.Constants;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Serialization.Contexts;

namespace Wumpex.Net.Gateway.Serialization;

/// <summary>
/// Serializes the <see cref="VoiceStateUpdateEvent" /> for receiving and sending from the gateway.
/// </summary>
public class VoiceStateUpdateEventSerializer : IEventSerializer<VoiceStateUpdateEvent>
{
	private readonly JsonTypeInfo<VoiceStateUpdateEvent> _typeInfo = GatewayEventSerializerContext.Default.VoiceStateUpdateEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(VoiceStateUpdateEvent @event)
	{
		using var memory = new MemoryStream();
		var writer = new Utf8JsonWriter(memory);
		writer.WriteStartObject();
		writer.WriteNumber("op", (int)GatewayOpcodes.VoiceStateUpdate);
		writer.WriteStartObject("d");
		writer.WriteString("guild_id", @event.GuildId);
		writer.WriteString("channel_id", @event.ChannelId);
		writer.WriteBoolean("self_mute", @event.SelfMute);
		writer.WriteBoolean("self_deaf", @event.SelfDeaf);
		writer.WriteEndObject();
		writer.WriteEndObject();

		writer.Flush();
		return memory.ToArray();
	}

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public VoiceStateUpdateEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;
}