using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Serialization.Contexts;

namespace Elixus.Discord.Gateway.Serialization;

internal sealed class InvalidSessionEventSerializer : IEventSerializer<InvalidSessionEvent>
{
	private readonly JsonTypeInfo<InvalidSessionEvent> _typeInfo = GatewayEventSerializerContext.Default.InvalidSessionEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(InvalidSessionEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event, _typeInfo);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public InvalidSessionEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;
}