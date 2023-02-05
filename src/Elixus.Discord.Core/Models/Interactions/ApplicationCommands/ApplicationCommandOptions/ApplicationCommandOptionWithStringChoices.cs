using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

/// <summary>
/// A subclass of <see cref="ApplicationCommandOption" /> that allows string <see cref="Choices" />.
/// </summary>
public sealed class ApplicationCommandOptionWithStringChoices : ApplicationCommandOption
{
	/// <summary>
	/// Choices for STRING types for the user to pick from, max 25.
	/// </summary>
	[JsonPropertyName("choices")]
	public List<ApplicationCommandOptionChoice<string>> Choices { get; set; } = null!;

	/// <summary>
	/// The minimum allowed length (minimum of 0, maximum of 6000).
	/// </summary>
	[JsonPropertyName("min_length")]
	public int MinLength { get; set; }

	/// <summary>
	/// The maximum allowed length (minimum of 1, maximum of 6000).
	/// </summary>
	[JsonPropertyName("max_length")]
	public int MaxLength { get; set; }

	/// <summary>
	/// If autocomplete interactions are enabled for this STRING type option.
	/// </summary>
	[JsonPropertyName("autocomplete")]
	public bool Autocomplete { get; set; }
}