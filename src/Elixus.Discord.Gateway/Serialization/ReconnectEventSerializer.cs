using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Serialization.Contexts;

namespace Elixus.Discord.Gateway.Serialization;

internal sealed class ReconnectEventSerializer : IEventSerializer<ReconnectEvent>
{
	private readonly JsonTypeInfo<ReconnectEvent> _typeInfo = GatewayEventSerializerContext.Default.ReconnectEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(ReconnectEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event, _typeInfo);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public ReconnectEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;
}