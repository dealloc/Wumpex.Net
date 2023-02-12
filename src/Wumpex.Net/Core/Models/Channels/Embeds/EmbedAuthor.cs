using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-author-structure" />
public class EmbedAuthor
{
	/// <summary>
	/// Name of author.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Url of author (only supports http(s)).
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Url of author icon (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("icon_url")]
	public string? IconUrl { get; set; }

	/// <summary>
	/// A proxied url of author icon.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	public string? ProxyIconUrl { get; set; }
}