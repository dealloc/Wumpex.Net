using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Stickers;

namespace Elixus.Discord.Core.Models.Stickers;

/// <summary>
/// The smallest amount of data required to render a sticker.
/// A partial sticker object.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/sticker#sticker-item-object" />
public class StickerItem
{
	/// <summary>
	/// id of the sticker.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// name of the sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Type of sticker format.
	/// </summary>
	[JsonPropertyName("format_type")]
	public StickerFormats FormatType { get; set; }
}