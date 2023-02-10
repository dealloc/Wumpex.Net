using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Events.Interactions;
using Elixus.Discord.Core.Models.Interactions;
using Elixus.Discord.Core.Serialization;
using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Serialization.Core;

/// <summary>
/// Handles serialization of <see cref="InteractionCreateEvent" />s.
/// </summary>
public class InteractionCreateEventSerializer : IEventSerializer<InteractionCreateEvent>
{
	private readonly JsonTypeInfo<Interaction> _typeInfo = EventSerializerContext.Default.Interaction;
	private readonly JsonTypeInfo<InteractionCreateEvent> _eventTypeInfo = EventSerializerContext.Default.InteractionCreateEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(InteractionCreateEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(@event.Interaction, _typeInfo!);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public InteractionCreateEvent Deserialize(ReadOnlySpan<byte> payload)
	{
		// Polymorphic serialization doesn't work if the root object is polymorphic.
		// So, we wrap it before deserializing
		// using var memory = new MemoryStream();
		// memory.Write("{\"interaction\":"u8);
		// memory.Write(payload);
		// memory.Write("}"u8);
		//
		// var buffer = memory.ToArray();
		// return JsonSerializer.Deserialize(buffer, _eventTypeInfo)!;
		return new InteractionCreateEvent()
		{
			Interaction = DeserializeInteraction(ref payload)
		};
	}

	/// <summary>
	/// Since the "Type" field is not first, polymorphic serialization does not work out of the box.
	/// </summary>
	private Interaction DeserializeInteraction(ref ReadOnlySpan<byte> payload)
	{
		var node = JsonNode.Parse(payload);

		return node["type"].Deserialize(EventSerializerContext.Default.InteractionTypes) switch
		{
			InteractionTypes.Ping => throw new NotSupportedException(),
			InteractionTypes.ApplicationCommand => node.Deserialize(EventSerializerContext.Default.ApplicationCommandInteraction),
			InteractionTypes.MessageComponent => node.Deserialize(EventSerializerContext.Default.MessageComponentInteraction),
			InteractionTypes.ApplicationCommandAutocomplete => node.Deserialize(EventSerializerContext.Default.ApplicationCommandAutocompleteInteraction),
			InteractionTypes.ModalSubmit => node.Deserialize(EventSerializerContext.Default.ModalSubmitInteraction),
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}