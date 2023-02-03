using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure" />
public class EmbedVideo
{
	/// <summary>
	/// Source url of video.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// A proxied url of the video.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public string? ProxyUrl { get; set; }

	/// <summary>
	/// Height of video.
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; set; }

	/// <summary>
	/// Width of video.
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; set; }
}