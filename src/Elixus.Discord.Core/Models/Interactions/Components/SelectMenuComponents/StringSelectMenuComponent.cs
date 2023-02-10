using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Emojis;

namespace Elixus.Discord.Core.Models.Interactions.Components.SelectMenuComponents;

/// <see href="https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-menu-structure" />
public class StringSelectMenuComponent : SelectMenuComponent
{
	/// <inheritdoc cref="Component.Type" />
	public override ComponentTypes Type { get; set; } = ComponentTypes.StringSelect;

	/// <summary>
	/// Specified choices in a select menu; max 25
	/// </summary>
	[JsonPropertyName("options")]
	public List<SelectOption> Options { get; set; } = new(0);
}

/// <see href="https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-option-structure" />
public class SelectOption
{
	/// <summary>
	/// User-facing name of the option; max 100 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public string Label { get; set; } = null!;

	/// <summary>
	/// Dev-defined value of the option; max 100 characters
	/// </summary>
	[JsonPropertyName("value")]
	public string Value { get; set; } = null!;

	/// <summary>
	/// Additional description of the option; max 100 characters.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// id, name, and animated.
	/// </summary>
	[JsonPropertyName("emoji")]
	public Emoji? Emoji { get; set; }

	/// <summary>
	/// Will show this option as selected by default.
	/// </summary>
	[JsonPropertyName("default")]
	public bool? Default { get; set; }
}