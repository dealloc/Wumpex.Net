namespace Elixus.Discord.Core.Constants.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level" />
public enum ExplicitContentFilterLevels
{
	/// <summary>
	/// media content will not be scanned.
	/// </summary>
	Disabled = 0,

	/// <summary>
	/// media content sent by members without roles will be scanned.
	/// </summary>
	MembersWithoutRoles = 1,

	/// <summary>
	/// media content sent by all members will be scanned.
	/// </summary>
	AllMembers = 2
}