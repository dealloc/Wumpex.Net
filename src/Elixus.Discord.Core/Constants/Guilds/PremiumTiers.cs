namespace Elixus.Discord.Core.Constants.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-object-premium-tier" />
public enum PremiumTiers
{
	/// <summary>
	/// Guild has not unlocked any Server Boost perks
	/// </summary>
	None = 0,

	/// <summary>
	/// Guild has unlocked Server Boost level 1 perks
	/// </summary>
	Tier1 = 1,

	/// <summary>
	/// guild has unlocked Server Boost level 2 perks.
	/// </summary>
	Tier2 = 2,

	/// <summary>
	/// guild has unlocked Server Boost level 3 perks.
	/// </summary>
	Tier3 = 3
}