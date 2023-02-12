using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-channel-structure" />
public sealed class WelcomeScreenChannel
{
	/// <summary>
	/// The channel's id.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; } = null!;

	/// <summary>
	/// The description shown for the channel.
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = null!;

	/// <summary>
	/// The emoji id, if the emoji is custom.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("emoji_id")]
	public string? EmojiId { get; set; }

	/// <summary>
	/// The emoji name if custom, the unicode character if standard, or null if no emoji is set.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public string? EmojiName { get; set; }
}