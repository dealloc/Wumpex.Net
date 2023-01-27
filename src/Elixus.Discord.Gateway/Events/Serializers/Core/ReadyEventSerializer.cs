using Elixus.Discord.Core.Events.Gateway;
using Elixus.Discord.Core.Serialization;
using Elixus.Discord.Gateway.Contracts.Events;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Elixus.Discord.Gateway.Events.Serializers.Core;

internal sealed class ReadyEventSerializer : IEventSerializer<ReadyEvent>
{
	private readonly JsonTypeInfo<ReadyEvent> _typeInfo = EventSerializerContext.Default.ReadyEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public ReadyEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(ReadyEvent @event)
		=> throw new NotImplementedException();
}
