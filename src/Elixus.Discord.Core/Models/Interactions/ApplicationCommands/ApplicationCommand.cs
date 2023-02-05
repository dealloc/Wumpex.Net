using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;

namespace Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

/// <see href="https://discord.com/developers/docs/interactions/application-commands#application-command-object" />
public class ApplicationCommand
{
	/// <summary>
	/// Unique ID of command.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// <see cref="ApplicationCommandTypes" />, defaults to <see cref="ApplicationCommandTypes.ChatInput" />.
	/// </summary>
	[JsonPropertyName("type")]
	public ApplicationCommandTypes Type { get; set; } = ApplicationCommandTypes.ChatInput;

	/// <summary>
	/// ID of the parent application.
	/// </summary>
	[JsonPropertyName("application_id")]
	public string ApplicationId { get; set; } = null!;

	/// <summary>
	/// Guild ID of the command, if not global.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// Name of command, 1-32 characters.
	/// </summary>
	/// <remarks>
	/// CHAT_INPUT command names and command option names must match the following regex ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$ with the unicode flag set.
	/// If there is a lowercase variant of any letters used, you must use those.
	/// Characters with no lowercase variants and/or uncased letters are still allowed.
	/// USER and MESSAGE commands may be mixed case and can include spaces.
	/// </remarks>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for <see cref="Name" /> field.
	/// Values follow the same restrictions as <see cref="Name" />.
	/// </summary>
	[JsonPropertyName("name_localizations")]
	public Dictionary<string, string>? NameLocalizations { get; set; }

	/// <summary>
	/// Description for CHAT_INPUT commands, 1-100 characters.
	/// Empty string for USER and MESSAGE commands.
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = null!;

	/// <summary>
	/// Localization dictionary for <see cref="Description" /> field.
	/// Values follow the same restrictions as <see cref="Description" />.
	/// </summary>
	[JsonPropertyName("description_localizations")]
	public Dictionary<string, string>? DescriptionLocalizations { get; set; }

	/// <summary>
	/// Parameters for the command, max of 25.
	/// </summary>
	[JsonPropertyName("options")]
	public List<ApplicationCommandOption> Options { get; set; } = new(0);

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
	/// Not recommended for use as field will soon be deprecated.
	/// Indicates whether the command is enabled by default when the app is added to a guild, defaults to true
	/// </summary>
	[Obsolete("Will be deprecated by Discord soon, recommend not using")]
	[JsonPropertyName("default_permission")]
	public bool? DefaultPermission { get; set; } = true;

	/// <summary>
	/// Indicates whether the command is age-restricted, defaults to false.
	/// </summary>
	[JsonPropertyName("nsfw")]
	public bool? Nsfw { get; set; }

	/// <summary>
	/// Auto incrementing version identifier updated during substantial record changes.
	/// </summary>
	public string Version { get; set; } = null!;
}
