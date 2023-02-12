using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands;
using Wumpex.Net.Api.Models.Channels;
using Wumpex.Net.Core.Constants.Channels;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Channels.Embeds;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;
using Wumpex.Net.Core.Models.Interactions.Components;

namespace Wumpex.Net.Api.Models.Interactions.InteractionResponses;

/// <summary>
/// Contains the response data for an <see cref="InteractionResponse" />.
/// </summary>
public abstract class InteractionCallbackData
{
	//
}

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-messages" />
/// <remarks>
/// Not all message fields are currently supported.
/// </remarks>
public class InteractionCallbackMessage : InteractionCallbackData
{
	/// <summary>
	/// Is the response TTS.
	/// </summary>
	[JsonPropertyName("tts")]
	public bool? Tts { get; set; }

	/// <summary>
	/// Message content
	/// </summary>
	[JsonPropertyName("content")]
	public string? Content { get; set; }

	/// <summary>
	/// Supports up to 10 embeds.
	/// </summary>
	[JsonPropertyName("embeds")]
	public List<Embed>? Embeds { get; set; }

	/// <summary>
	/// Allowed mentions object.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public AllowedMention? AllowedMentions { get; set; }

	/// <summary>
	/// Message flags combined as a bitfield (only <see cref="MessageFlags.SuppressEmbeds" /> and <see cref="MessageFlags.Ephemeral" /> can be set).
	/// </summary>
	[JsonPropertyName("flags")]
	public MessageFlags? Flags { get; set; }

	/// <summary>
	/// Message components.
	/// </summary>
	[JsonPropertyName("components")]
	public List<Component>? Components { get; set; }

	/// <summary>
	/// Attachment objects with filename and description.
	/// </summary>
	[JsonPropertyName("attachments")]
	public List<Attachment>? Attachments { get; set; }
}

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-autocomplete" />
public class InteractionCallbackAutocomplete : InteractionCallbackData
{
	/// <summary>
	/// Autocomplete choices (max of 25 choices).
	/// </summary>
	[JsonPropertyName("choices")]
	public List<ApplicationCommandOptionWithOptions> Choices { get; set; } = new(0);
}

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-modal" />
public class InteractionCallbackModal : InteractionCallbackData
{
	/// <summary>
	/// A developer-defined identifier for the modal, max 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string CustomId { get; set; } = null!;

	/// <summary>
	/// The title of the popup modal, max 45 characters.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = null!;

	/// <summary>
	/// Between 1 and 5 (inclusive) components that make up the modal.
	/// </summary>
	[JsonPropertyName("components")]
	public List<Component> Components { get; set; } = new(0);
}