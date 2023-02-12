using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-emoji" />
public sealed class ActivityEmoji
{
	/// <summary>
	/// Name of the emoji.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// ID of the emoji.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// Whether the emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public bool? Animated { get; set; }
}