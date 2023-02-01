namespace Elixus.Discord.Core.Constants.Activities;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-flags" />
[Flags]
public enum ActivityFlags
{
	/// <summary>
	/// INSTANCE
	/// </summary>
	Instance = 1 << 0,

	/// <summary>
	/// JOIN
	/// </summary>
	Join = 1 << 1,

	/// <summary>
	/// SPECTATE
	/// </summary>
	Spectate = 1 << 2,

	/// <summary>
	/// JOIN_REQUEST
	/// </summary>
	JoinRequest = 1 << 3,

	/// <summary>
	/// SYNC
	/// </summary>
	Sync = 1 << 4,

	/// <summary>
	/// PLAY
	/// </summary>
	Play = 1 << 5,

	/// <summary>
	/// PARTY_PRIVACY_FRIENDS
	/// </summary>
	PartyPrivacyFriends = 1 << 6,

	/// <summary>
	/// PARTY_PRIVACY_VOICE_CHANNEL
	/// </summary>
	PartyPrivacyVoiceChannel = 1 << 7,

	/// <summary>
	/// EMBEDDED
	/// </summary>
	Embedded = 1 << 8
}