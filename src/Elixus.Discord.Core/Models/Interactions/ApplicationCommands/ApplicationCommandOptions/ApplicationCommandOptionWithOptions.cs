using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

/// <summary>
/// A subclass of <see cref="ApplicationCommand" /> with sub <see cref="ApplicationCommand" />.
/// </summary>
public class ApplicationCommandOptionWithOptions : ApplicationCommandOption
{
	/// <summary>
	/// If the option is a subcommand or subcommand group type, these nested options will be the parameters.
	/// </summary>
	[JsonPropertyName("options")]
	public List<ApplicationCommandOption> Options { get; set; } = new(0);
}

/// <summary>
/// Essentially the same class as <see cref="ApplicationCommandOptionWithOptions" />.
/// This type is a placeholder so that JsonDerivedType works, since it disallows the same type twice.
/// </summary>
public sealed class ApplicationCommandOptionGroupWithOptions : ApplicationCommandOptionWithOptions
{
	//
}