using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Core.Serialization;
using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Serialization;

/// <summary>
/// Whenever an event has no specific serialization needs this serializer takes care of serializing and deserializing.
/// </summary>
internal sealed class CoreEventSerializer<TEvent> : IEventSerializer<TEvent> where TEvent : class, new()
{
	private readonly JsonTypeInfo<TEvent>? _typeInfo = (JsonTypeInfo<TEvent>)EventSerializerContext.Default.GetTypeInfo(typeof(TEvent));

	public CoreEventSerializer()
	{
		if (_typeInfo is null)
			throw new NullReferenceException($"Could not find typeinfo for {typeof(TEvent).FullName}");
	}

	public ArraySegment<byte> Serialize(TEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event, _typeInfo!);

	public TEvent Deserialize(ReadOnlySpan<byte> payload)
		=> JsonSerializer.Deserialize(payload, _typeInfo!)!;
}