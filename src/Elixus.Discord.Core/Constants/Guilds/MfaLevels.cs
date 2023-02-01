namespace Elixus.Discord.Core.Constants.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-object-mfa-level" />
public enum MfaLevels
{
	/// <summary>
	/// guild has no MFA/2FA requirement for moderation actions.
	/// </summary>
	None = 0,

	/// <summary>
	/// guild has a 2FA requirement for moderation actions.
	/// </summary>
	Elevated = 1
}