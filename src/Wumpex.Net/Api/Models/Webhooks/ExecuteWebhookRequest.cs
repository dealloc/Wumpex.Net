using System.Text.Json.Serialization;
using Wumpex.Net.Api.Models.Channels;
using Wumpex.Net.Core.Constants.Channels;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Channels.Embeds;
using Wumpex.Net.Core.Models.Interactions.Components;

namespace Wumpex.Net.Api.Models.Webhooks;

/// <see href="https://discord.com/developers/docs/resources/webhook#execute-webhook" />
public sealed class ExecuteWebhookRequest
{
	/// <summary>
	/// The id of the thread the message is in.
	/// </summary>
	/// <see href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message-query-string-params" />
	[JsonIgnore]
	public string? ThreadId { get; set; }

	/// <summary>
	/// waits for server confirmation of message send before response, and returns the created message body
	/// (defaults to <c>false</c>; when <c>false</c> a message that is not saved does not return an error)
	/// </summary>
	[JsonIgnore]
	public bool? Wait { get; set; }

	/// <summary>
	/// The message contents (up to 2000 characters).
	/// </summary>
	[JsonPropertyName("content")]
	public string? Content { get; set; }

	/// <summary>
	/// Override the default username of the webhook.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; set; }

	/// <summary>
	/// Override the default avatar of the webhook.
	/// </summary>
	[JsonPropertyName("avatar_url")]
	public string? AvatarUrl { get; set; }

	/// <summary>
	/// <c>true</c> if this is a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public bool? TTS { get; set; }

	/// <summary>
	/// Embedded rich content.
	/// </summary>
	/// <remarks>
	/// For the webhook embed objects, you can set every field except type (it will be rich regardless of if you try to set it), provider, video, and any height, width, or proxy_url values for images.
	/// </remarks>
	[JsonPropertyName("embeds")]
	public List<Embed>? Embeds { get; set; }

	/// <summary>
	/// Allowed mentions for the message.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public AllowedMention? AllowedMentions { get; set; }

	/// <summary>
	/// The components to include with the message.
	/// </summary>
	[JsonPropertyName("components")]
	public List<Component>? Components { get; set; }

	/// <summary>
	/// Contents of the files being sent.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#uploading-files" />
	[JsonIgnore]
	public List<byte[]>? Files { get; set; }

	/// <summary>
	/// Attached files to keep and possible descriptions for new files.
	/// </summary>
	[JsonPropertyName("attachments")]
	public List<Attachment>? Attachments { get; set; }

	/// <summary>
	/// message flags combined as a bitfield (only <see cref="MessageFlags.SuppressEmbeds" /> can be set).
	/// </summary>
	[JsonPropertyName("flags")]
	public MessageFlags? Flags { get; set; }

	/// <summary>
	/// Name of thread to create
	/// (requires the webhook channel to be a forum channel)
	/// </summary>
	[JsonPropertyName("thread_name")]
	public string? ThreadName { get; set; }
}