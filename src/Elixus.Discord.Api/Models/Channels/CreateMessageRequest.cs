using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Channels;
using Elixus.Discord.Core.Models.Channels;
using Elixus.Discord.Core.Models.Channels.Embeds;
using Elixus.Discord.Core.Models.Interactions.Components;
using Elixus.Discord.Core.Serialization.Converters;

namespace Elixus.Discord.Api.Models.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#create-message" />
public class CreateMessageRequest
{
	/// <summary>
	/// Message contents (up to 2000 characters).
	/// </summary>
	[JsonPropertyName("content")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Content { get; set; }

	/// <summary>
	/// Can be used to verify a message was sent (up to 25 characters).
	/// Value will appear in the Message Create event.
	/// </summary>
	[JsonPropertyName("nonce")]
	[JsonConverter(typeof(NumberOrStringConverter))]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Nonce { get; set; }

	/// <summary>
	/// <c>true</c> if this is a TTS message
	/// </summary>
	[JsonPropertyName("tts")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? TTS { get; set; }

	/// <summary>
	/// Embedded rich content (up to 6000 characters).
	/// </summary>
	[JsonPropertyName("embeds")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<Embed>? Embeds { get; set; }

	/// <summary>
	/// Allowed mentions for the message.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public AllowedMention? AllowedMentions { get; set; }

	/// <summary>
	/// Include to make your message a reply.
	/// </summary>
	[JsonPropertyName("message_reference")]
	public MessageReference? MessageReference { get; set; }

	/// <summary>
	/// Components to include with the message.
	/// </summary>
	[JsonPropertyName("components")]
	public List<Component>? Components { get; set; }

	/// <summary>
	/// IDs of up to 3 stickers in the server to send in the message.
	/// </summary>
	[JsonPropertyName("sticker_ids")]
	public List<string>? StickerIds { get; set; }

	/// <summary>
	/// Contents of the files being sent.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#uploading-files" />
	[JsonIgnore]
	public List<byte[]>? Files { get; set; }

	/// <summary>
	/// Attachment objects with filename and description.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#uploading-files" />
	[JsonPropertyName("attachments")]
	public List<Attachment>? Attachments { get; set; }

	/// <summary>
	/// Message flags combined as a bitfield
	/// (only <see cref="MessageFlags.SuppressEmbeds" /> can be set).
	/// </summary>
	[JsonPropertyName("flags")]
	public MessageFlags? Flags { get; set; }
}