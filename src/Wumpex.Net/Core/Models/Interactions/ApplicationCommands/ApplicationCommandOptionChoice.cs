using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Interactions.ApplicationCommands;

/// <summary>
/// If you specify choices for an option, they are the only valid values for a user to pick
/// </summary>
/// <see href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure" />
public sealed class ApplicationCommandOptionChoice<TValue>
{
	/// <summary>
	/// 1-100 character choice name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for the name field. Values follow the same restrictions as name.
	/// </summary>
	[JsonPropertyName("name_localizations")]
	public Dictionary<string, string>? NameLocalizations { get; set; }

	/// <summary>
	/// Value for the choice, up to 100 characters if string.
	/// </summary>
	/// <remarks>
	/// Can be either string, int or double.
	/// </remarks>
	[JsonPropertyName("value")]
	public TValue Value { get; set; } = default!; 
}
