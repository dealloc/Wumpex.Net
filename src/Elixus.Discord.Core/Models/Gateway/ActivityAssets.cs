using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-assets" />
public sealed class ActivityAssets
{
	/// <summary>
	/// See Activity Asset Image.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image" />
	[JsonPropertyName("large_image")]
	public string? LargeImage { get; set; }

	/// <summary>
	/// Text displayed when hovering over the large image of the activity.
	/// </summary>
	[JsonPropertyName("large_text")]
	public string? LargeText { get; set; }

	/// <summary>
	/// See Activity Asset Image.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image" />
	[JsonPropertyName("small_image")]
	public string? SmallImage { get; set; }

	/// <summary>
	/// Text displayed when hovering over the small image of the activity.
	/// </summary>
	[JsonPropertyName("small_text")]
	public string? SmallText { get; set; }
}