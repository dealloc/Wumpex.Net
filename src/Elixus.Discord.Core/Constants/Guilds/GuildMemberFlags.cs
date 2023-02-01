namespace Elixus.Discord.Core.Constants.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-flags" />
[Flags]
public enum GuildMemberFlags
{
	/// <summary>
	/// Represents no flags active.
	/// </summary>
	None = 0,

	/// <summary>
	/// Member has left and rejoined the guild.
	/// </summary>
	DidRejoin = 1 << 0,

	/// <summary>
	/// Member has completed onboarding.
	/// </summary>
	CompletedOnboarding = 1 << 1,

	/// <summary>
	/// Member is exempt from guild verification requirements.
	/// </summary>
	BypassesVerification = 1 << 2,

	/// <summary>
	/// Member has started onboarding.
	/// </summary>
	StartedOnboarding = 1 << 3
}