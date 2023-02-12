using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-thumbnail-structure" />
public class EmbedThumbnail
{
	/// <summary>
	/// Source url of thumbnail (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = null!;

	/// <summary>
	/// A proxied url of the thumbnail.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public string? ProxyUrl { get; set; }

	/// <summary>
	/// Height of thumbnail.
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; set; }

	/// <summary>
	/// Width of thumbnail.
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; set; }
}