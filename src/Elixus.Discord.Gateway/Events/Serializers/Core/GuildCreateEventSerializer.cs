using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Core.Events.Guilds;
using Elixus.Discord.Core.Serialization;
using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Events.Serializers.Core;

internal sealed class GuildCreateEventSerializer : IEventSerializer<GuildCreateEvent>
{
	private readonly JsonTypeInfo<GuildCreateEvent> _typeInfo = EventSerializerContext.Default.GuildCreateEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public GuildCreateEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(GuildCreateEvent @event)
		=> throw new NotImplementedException();
}