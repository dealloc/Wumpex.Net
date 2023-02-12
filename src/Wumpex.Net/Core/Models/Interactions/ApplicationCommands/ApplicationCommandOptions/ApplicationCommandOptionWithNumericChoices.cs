using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

/// <summary>
/// A subclass of <see cref="ApplicationCommandOption" /> that allows numerical <see cref="Choices" />.
/// </summary>
public sealed class ApplicationCommandOptionWithNumericChoices<TValue> : ApplicationCommandOption where TValue : notnull
{
	/// <summary>
	/// Choices for INTEGER, and NUMBER types for the user to pick from, max 25.
	/// </summary>
	[JsonPropertyName("choices")]
	public List<ApplicationCommandOptionChoice<TValue>> Choices { get; set; } = null!;

	/// <summary>
	/// If the option is an INTEGER or NUMBER type, the minimum value permitted.
	/// </summary>
	[JsonPropertyName("min_value")]
	public TValue? MinValue { get; set; } = default;

	/// <summary>
	/// If the option is an INTEGER or NUMBER type, the maximum value permitted.
	/// </summary>
	[JsonPropertyName("max_value")]
	public TValue? MaxValue { get; set; } = default;

	/// <summary>
	/// If autocomplete interactions are enabled for this STRING type option.
	/// </summary>
	[JsonPropertyName("autocomplete")]
	public bool Autocomplete { get; set; }
}