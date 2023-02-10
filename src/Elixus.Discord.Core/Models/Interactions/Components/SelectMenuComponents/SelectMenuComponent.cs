using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Interactions.Components.SelectMenuComponents;

/// <summary>
/// 
/// </summary>
public abstract class SelectMenuComponent : Component
{
	/// <summary>
	/// ID for the select menu; max 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string CustomId { get; set; } = null!;

	/// <summary>
	/// Placeholder text if nothing is selected;
	/// max 150 characters.
	/// </summary>
	[JsonPropertyName("placeholder")]
	public string? Placeholder { get; set; }

	/// <summary>
	/// Minimum number of items that must be chosen (defaults to 1);
	/// min 0, max 25.
	/// </summary>
	[JsonPropertyName("min_values")]
	public int? MinValues { get; set; }

	/// <summary>
	/// Maximum number of items that can be chosen (defaults to 1);
	/// max 25.
	/// </summary>
	[JsonPropertyName("max_values")]
	public int? MaxValues { get; set; }

	/// <summary>
	/// Whether select menu is disabled (defaults to false).
	/// </summary>
	[JsonPropertyName("disabled")]
	public bool? Disabled { get; set; } = false;
}