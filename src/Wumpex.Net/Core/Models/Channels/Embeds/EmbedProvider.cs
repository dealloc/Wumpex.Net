using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-provider-structure" />
public class EmbedProvider
{
	/// <summary>
	/// Name of provider.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Url of provider.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }
}