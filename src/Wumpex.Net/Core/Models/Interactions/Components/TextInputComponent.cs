using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions.Components;

/// <summary>
/// Text inputs are an interactive component that render on modals.
/// They can be used to collect short-form or long-form text.
/// </summary>
/// <see href="https://discord.com/developers/docs/interactions/message-components#text-inputs" />
public class TextInputComponent : Component
{
	/// <summary>
	/// The type of <see cref="Component" />.
	/// </summary>
	[JsonPropertyName("type")]
	public override ComponentTypes Type { get; set; } = ComponentTypes.TextInput;

	/// <summary>
	/// Developer-defined identifier for the input; max 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string CustomId { get; set; } = null!;

	/// <summary>
	/// The Text Input Style.
	/// </summary>
	[JsonPropertyName("style")]
	public TextInputStyles Style { get; set; }

	/// <summary>
	/// Label for this component;
	/// max 45 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public string Label { get; set; } = null!;

	/// <summary>
	/// Minimum input length for a text input; min 0, max 4000.
	/// </summary>
	[JsonPropertyName("min_length")]
	public int? MinLength { get; set; }

	/// <summary>
	/// Maximum input length for a text input; min 1, max 4000.
	/// </summary>
	[JsonPropertyName("max_length")]
	public int? MaxLength { get; set; }

	/// <summary>
	/// Whether this component is required to be filled (defaults to true).
	/// </summary>
	[JsonPropertyName("required")]
	public bool? Required { get; set; } = true;

	/// <summary>
	/// Pre-filled value for this component; max 4000 characters.
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }

	/// <summary>
	/// Custom placeholder text if the input is empty; max 100 characters.
	/// </summary>
	[JsonPropertyName("placeholder")]
	public string? Placeholder { get; set; }
}