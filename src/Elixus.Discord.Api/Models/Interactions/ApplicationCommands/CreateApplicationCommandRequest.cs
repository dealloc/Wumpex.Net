using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

namespace Elixus.Discord.Api.Models.Interactions.ApplicationCommands;

/// <summary>
/// 
/// </summary>
public sealed class CreateApplicationCommandRequest
{
	/// <summary>
	/// Name of command, 1-32 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for the name field.
	/// Values follow the same restrictions as name.
	/// </summary>
	/// <remarks>
	/// Available localizations can be found <a href="https://discord.com/developers/docs/reference#locales">here</a>
	/// </remarks>
	[JsonPropertyName("name_localizations")]
	public Dictionary<string, string>? NameLocalizations { get; set; }

	/// <summary>
	/// 1-100 character description for CHAT_INPUT commands.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Localization dictionary for the description field.
	/// Values follow the same restrictions as description
	/// </summary>
	[JsonPropertyName("description_localizations")]
	public Dictionary<string, string>? DescriptionLocalizations { get; set; }

	/// <summary>
	/// The parameters for the command.
	/// </summary>
	[JsonPropertyName("options")]
	public List<ApplicationCommandOption>? Options { get; set; }

	/// <summary>
	/// Set of permissions represented as a bit set.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/permissions" />
	[JsonPropertyName("default_member_permissions")]
	public string? DefaultMemberPermissions { get; set; }

	/// <summary>
	/// Indicates whether the command is available in DMs with the app, only for globally-scoped commands.
	/// By default, commands are visible.
	/// </summary>
	[JsonPropertyName("dm_permission")]
	public bool? DmPermission { get; set; }

	/// <summary>
	/// Replaced by default_member_permissions and will be deprecated in the future.
	/// Indicates whether the command is enabled by default when the app is added to a guild. Defaults to true
	/// </summary>
	[JsonPropertyName("default_permission")]
	public bool? DefaultPermission { get; set; }

	/// <summary>
	/// Type of command, defaults 1 if not set.
	/// </summary>
	[JsonPropertyName("type")]
	public ApplicationCommandTypes Type { get; set; } = ApplicationCommandTypes.ChatInput;

	/// <summary>
	/// Indicates whether the command is age-restricted.
	/// </summary>
	[JsonPropertyName("nsfw")]
	public bool? Nsfw { get; set; }
}