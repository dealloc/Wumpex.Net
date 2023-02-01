namespace Elixus.Discord.Core.Constants.Stages;

/// <see href="https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-privacy-level" />
public enum StagePrivacyLevel
{
	/// <summary>
	/// The Stage instance is visible publicly. (deprecated)
	/// </summary>
	[Obsolete("Deprecated according to Discord documentation")]
	Public = 1,

	/// <summary>
	/// The Stage instance is visible to only guild members.
	/// </summary>
	GuildOnly = 2
}