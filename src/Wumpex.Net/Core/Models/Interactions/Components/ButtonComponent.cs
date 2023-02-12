using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;
using Wumpex.Net.Core.Models.Emojis;

namespace Wumpex.Net.Core.Models.Interactions.Components;

/// <summary>
/// Buttons are interactive components that render in messages. They can be clicked by users, and send an interaction to your app when clicked.
/// </summary>
public sealed class ButtonComponent : Component
{
	/// <inheritdoc cref="Component.Type" />
	public override ComponentTypes Type { get; set; } = ComponentTypes.Button;

	/// <summary>
	/// A button style.
	/// </summary>
	[JsonPropertyName("style")]
	public ButtonStyles Style { get; set; }

	/// <summary>
	/// Text that appears on the button; max 80 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public string? Label { get; set; }

	/// <summary>
	/// name, id, and animated.
	/// </summary>
	[JsonPropertyName("emoji")]
	public Emoji? Emoji { get; set; }

	/// <summary>
	/// Developer-defined identifier for the button;
	/// Max 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string? CustomId { get; set; }

	/// <summary>
	/// URL for link-style buttons.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Whether the button is disabled (defaults to false).
	/// </summary>
	[JsonPropertyName("disabled")]
	public bool? Disabled { get; set; } = false;
}