using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Stickers;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Models.Stickers;

/// <see href="https://discord.com/developers/docs/resources/sticker#sticker-object" />
public sealed class Sticker : StickerItem
{
	/// <summary>
	/// For standard stickers, id of the pack the sticker is from.
	/// </summary>
	[JsonPropertyName("pack_id")]
	public string? PackId { get; set; }

	/// <summary>
	/// description of the sticker
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; } = null!;

	/// <summary>
	/// autocomplete/suggestion tags for the sticker (max 200 characters)
	/// </summary>
	/// <remarks>
	/// A comma separated list of keywords is the format used in this field by standard stickers, but this is just a convention.
	/// Incidentally the client will always use a name generated from an emoji as the value of this field when creating or modifying a guild sticker.
	/// </remarks>
	[JsonPropertyName("tags")]
	public string Tags { get; set; } = null!;

	/// <summary>
	/// Deprecated previously the sticker asset hash, now an empty string.
	/// </summary>
	[Obsolete("Deprecated according to the Discord documentation")]
	[JsonPropertyName("asset")]
	public string? Asset { get; set; }

	/// <summary>
	/// Type of sticker.
	/// </summary>
	[JsonPropertyName("type")]
	public StickerTypes Type { get; set; }

	/// <summary>
	/// whether this guild sticker can be used, may be false due to loss of Server Boosts.
	/// </summary>
	[JsonPropertyName("available")]
	public bool? Available { get; set; }

	/// <summary>
	/// id of the guild that owns this sticker.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// the user that uploaded the guild sticker.
	/// </summary>
	[JsonPropertyName("user")]
	public User? User { get; set; }

	/// <summary>
	/// the standard sticker's sort order within its pack.
	/// </summary>
	[JsonPropertyName("sort_value")]
	public int? SortValue { get; set; }
}