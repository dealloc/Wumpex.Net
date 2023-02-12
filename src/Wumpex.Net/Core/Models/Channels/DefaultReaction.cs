using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels;

/// <summary>
/// An object that specifies the emoji to use as the default way to react to a forum post. Exactly one of emoji_id and emoji_name must be set.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/resources/channel#default-reaction-object" />
public sealed class DefaultReaction
{
	/// <summary>
	/// The id of a guild's custom emoji.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public string? EmojiId { get; set; }

	/// <summary>
	/// The unicode character of the emoji.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public string? EmojiName { get; set; }
}