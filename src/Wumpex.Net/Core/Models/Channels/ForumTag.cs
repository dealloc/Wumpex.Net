using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels;

/// <summary>
/// An object that represents a tag that is able to be applied to a thread in a GUILD_FORUM channel.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/channel#forum-tag-object" />
public sealed class ForumTag
{
	/// <summary>
	/// The id of the tag.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The name of the tag (0-20 characters).
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Whether this tag can only be added to or removed from threads by a member with the MANAGE_THREADS permission.
	/// </summary>
	[JsonPropertyName("moderated")]
	public bool Moderated { get; set; }

	/// <summary>
	/// The id of a guild's custom emoji *
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public string? EmojiId { get; set; }

	/// <summary>
	/// The unicode character of the emoji.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public string? EmojiName { get; set; }
}