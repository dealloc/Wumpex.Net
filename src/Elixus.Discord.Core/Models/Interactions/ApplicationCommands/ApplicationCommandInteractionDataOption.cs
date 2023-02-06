using System.Diagnostics;
using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Serialization.Converters;

namespace Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

/// <summary>
/// All options have names, and an option can either be a parameter and input value --in which case value will be set
/// --or it can denote a subcommand or group
/// --in which case it will contain a top-level key and another array of options.
/// </summary>
[DebuggerDisplay("{Name} -> {Value}")]
public class ApplicationCommandInteractionDataOption
{
	/// <summary>
	/// Name of the parameter
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Value of <see cref="ApplicationCommandOptionTypes" />.
	/// </summary>
	[JsonPropertyName("type")]
	public ApplicationCommandOptionTypes Type { get; set; }

	/// <summary>
	/// Value of the option resulting from user input.
	/// </summary>
	/// <remarks>
	/// Despite being string type, the value could be a int, long or double.
	/// </remarks>
	[JsonPropertyName("value")]
	[JsonConverter(typeof(NumberOrStringConverter))]
	public string? Value { get; set; }

	/// <summary>
	/// Present if this option is a group or subcommand.
	/// </summary>
	[JsonPropertyName("options")]
	public List<ApplicationCommandInteractionDataOption>? Options { get; set; }

	/// <summary>
	/// <c>true</c> if this option is the currently focused option for autocomplete.
	/// </summary>
	[JsonPropertyName("focused")]
	public bool? Focused { get; set; }
}