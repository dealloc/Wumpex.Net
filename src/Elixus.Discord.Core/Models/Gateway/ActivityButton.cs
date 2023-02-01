using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-buttons" />
public sealed class ActivityButton
{
	/// <summary>
	/// Text shown on the button (1-32 characters).
	/// </summary>
	[JsonPropertyName("label")]
	public string Label { get; set; } = null!;

	/// <summary>
	/// URL opened when clicking the button (1-512 characters).
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = null!;
}