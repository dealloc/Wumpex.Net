using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure" />
public class EmbedFooter
{
	/// <summary>
	/// Footer text.
	/// </summary>
	[JsonPropertyName("text")]
	public string Text { get; set; } = null!;

	/// <summary>
	/// Url of footer icon (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("icon_url")]
	public string? IconUrl { get; set; }

	/// <summary>
	/// A proxied url of footer icon.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	public string? ProxyIconUrl { get; set; }
}