using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

namespace Wumpex.Net.Core.Models.Interactions.ApplicationCommands;

/// <see href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-structure" />
/// <remarks>
/// Required options must be listed before optional options.
/// </remarks>
[JsonDerivedType(typeof(ApplicationCommandOptionWithStringChoices), (int)ApplicationCommandOptionTypes.String)]
[JsonDerivedType(typeof(ApplicationCommandOptionWithNumericChoices<long>), (int)ApplicationCommandOptionTypes.Integer)]
[JsonDerivedType(typeof(ApplicationCommandOptionWithNumericChoices<double>), (int)ApplicationCommandOptionTypes.Number)]
[JsonDerivedType(typeof(ApplicationCommandOptionWithOptions), (int)ApplicationCommandOptionTypes.SubCommand)]
[JsonDerivedType(typeof(ApplicationCommandOptionGroupWithOptions), (int)ApplicationCommandOptionTypes.SubCommandGroup)]
[JsonDerivedType(typeof(ApplicationCommandOptionWithChannelTypes), (int)ApplicationCommandOptionTypes.Channel)]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType, IgnoreUnrecognizedTypeDiscriminators = true)]
public class ApplicationCommandOption
{
	/// <summary>
	/// Type of option.
	/// </summary>
	[JsonPropertyName("type")]
	public virtual ApplicationCommandOptionTypes Type { get; set; }

	/// <summary>
	/// 1-32 character name.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-naming" />
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for the <see cref="Name" /> field.
	/// Values follow the same restrictions as <see cref="Name" />.
	/// </summary>
	[JsonPropertyName("name_localizations")]
	public Dictionary<string, string>? NameLocalizations { get; set; }

	/// <summary>
	/// 1-100 character description.
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for the <see cref="Description" /> field.
	/// Values follow the same restrictions as <see cref="Description" />.
	/// </summary>
	[JsonPropertyName("description_localizations")]
	public Dictionary<string, string>? DescriptionLocalizations { get; set; }

	/// <summary>
	/// If the parameter is required or optional--default false
	/// </summary>
	[JsonPropertyName("required")]
	public bool? Required { get; set; }
}