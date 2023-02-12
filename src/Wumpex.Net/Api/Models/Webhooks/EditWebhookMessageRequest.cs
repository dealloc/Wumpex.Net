using System.Text.Json.Serialization;
using Wumpex.Net.Api.Models.Channels;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Channels.Embeds;
using Wumpex.Net.Core.Models.Interactions.Components;

namespace Wumpex.Net.Api.Models.Webhooks;

/// <summary>
/// Edits a previously-sent webhook message from the same token.
/// When the content field is edited, the mentions array in the message object will be reconstructed from scratch based on the new content.
/// The allowed_mentions field of the edit request controls how this happens.
/// If there is no explicit allowed_mentions in the edit request, the content will be parsed with default allowances, that is, without regard to whether or not an allowed_mentions was present in the request that originally created the message.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message" />
public class EditWebhookMessageRequest
{
	/// <summary>
	/// The id of the thread the message is in.
	/// </summary>
	/// <see href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message-query-string-params" />
	[JsonIgnore]
	public string? ThreadId { get; set; }

	/// <summary>
	/// The message contents (up to 2000 characters).
	/// </summary>
	[JsonPropertyName("content")]
	public string? Content { get; set; }

	/// <summary>
	/// Embed <c>rich</c> content.
	/// </summary>
	[JsonPropertyName("embeds")]
	public List<Embed>? Embeds { get; set; } = new(0);

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
}