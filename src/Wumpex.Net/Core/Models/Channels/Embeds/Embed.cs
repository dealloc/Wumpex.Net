using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object" />
public class Embed
{
	/// <summary>
	/// Title of embed.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Type of embed (always "rich" for webhook embeds).
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/channel#embed-object-embed-types" />
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Description of embed.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Url of embed.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Timestamp of embed content.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public DateTime? Timestamp { get; set; }

	/// <summary>
	/// Color code of the embed.
	/// </summary>
	[JsonPropertyName("color")]
	public int? Color { get; set; }

	/// <summary>
	/// Footer information.
	/// </summary>
	[JsonPropertyName("footer")]
	public EmbedFooter? Footer { get; set; }

	/// <summary>
	/// Image information.
	/// </summary>
	[JsonPropertyName("image")]
	public EmbedImage? Image { get; set; }

	/// <summary>
	/// Thumbnail information.
	/// </summary>
	[JsonPropertyName("thumbnail")]
	public EmbedThumbnail? Thumbnail { get; set; }

	/// <summary>
	/// Video information.
	/// </summary>
	[JsonPropertyName("video")]
	public EmbedVideo? Video { get; set; }

	/// <summary>
	/// Provider information.
	/// </summary>
	[JsonPropertyName("provider")]
	public EmbedProvider? Provider { get; set; }

	/// <summary>
	/// Author information.
	/// </summary>
	[JsonPropertyName("author")]
	public EmbedAuthor? Author { get; set; }

	/// <summary>
	/// Fields information.
	/// </summary>
	[JsonPropertyName("fields")]
	public List<EmbedField> Fields { get; set; } = new(0);
}