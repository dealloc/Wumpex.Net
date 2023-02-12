namespace Wumpex.Net.Core.Constants.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#channel-object-channel-flags" />
[Flags]
public enum ChannelFlags
{
	/// <summary>
	/// this thread is pinned to the top of its parent GUILD_FORUM channel.
	/// </summary>
	Pinned = 1 << 1,

	/// <summary>
	/// whether a tag is required to be specified when creating a thread in a GUILD_FORUM channel.
	/// Tags are specified in the applied_tags field.
	/// </summary>
	RequireTag = 1 << 4
}