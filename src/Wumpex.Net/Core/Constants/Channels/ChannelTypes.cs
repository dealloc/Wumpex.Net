namespace Wumpex.Net.Core.Constants.Channels;

/// <remarks>
/// Type <see cref="AnnouncementThread" />, <see cref="PublicThread" /> and <see cref="PrivateThread" /> are only available in API v9 and above.
/// </remarks>
/// <see href="https://discord.com/developers/docs/resources/channel#channel-object-channel-types" />
public enum ChannelTypes
{
	/// <summary>
	/// A text channel within a server.
	/// </summary>
	GuildText = 0,

	/// <summary>
	/// A direct message between users.
	/// </summary>
	Dm = 1,

	/// <summary>
	/// A voice channel within a server.
	/// </summary>
	GuildVoice = 2,

	/// <summary>
	/// A direct message between multiple users
	/// </summary>
	GroupDm = 3,

	/// <summary>
	/// an organizational category that contains up to 50 channels
	/// </summary>
	GuildCategory = 4,

	/// <summary>
	/// A channel that users can follow and crosspost into their own server (formerly news channels)
	/// </summary>
	GuildAnnouncement = 5,

	/// <summary>
	/// A temporary sub-channel within a GUILD_ANNOUNCEMENT channel
	/// </summary>
	AnnouncementThread = 10,

	/// <summary>
	/// A temporary sub-channel within a GUILD_TEXT or GUILD_FORUM channel
	/// </summary>
	PublicThread = 11,

	/// <summary>
	/// A temporary sub-channel within a GUILD_TEXT channel that is only viewable by those invited and those with the MANAGE_THREADS permission
	/// </summary>
	PrivateThread = 12,

	/// <summary>
	/// a voice channel for hosting events with an audience
	/// </summary>
	GuildStageVoice = 13,

	/// <summary>
	/// The channel in a hub containing the listed servers
	/// </summary>
	GuildDirectory = 14,

	/// <summary>
	/// Channel that can only contain threads.
	/// </summary>
	GuildForum = 15
}