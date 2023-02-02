using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Serialization.Contexts;

namespace Elixus.Discord.Gateway.Serialization;

/// <summary>
/// Handles the serialization of the <see cref="HelloEvent" />.
/// </summary>
internal sealed class HelloEventSerializer : IEventSerializer<HelloEvent>
{
	private readonly JsonTypeInfo<HelloEvent> _typeInfo = GatewayEventSerializerContext.Default.HelloEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(HelloEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event, _typeInfo);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public HelloEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo)!;
}