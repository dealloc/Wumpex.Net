using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Serialization.Contexts;

namespace Wumpex.Net.Gateway.Serialization;

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