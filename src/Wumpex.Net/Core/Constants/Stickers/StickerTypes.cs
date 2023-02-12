namespace Wumpex.Net.Core.Constants.Stickers;

/// <see href="https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-types" />
public enum StickerTypes
{
	/// <summary>
	/// An official sticker in a pack, part of Nitro or in a removed purchasable pack.
	/// </summary>
	Standard = 1,

	/// <summary>
	/// A sticker uploaded to a guild for the guild's members.
	/// </summary>
	Guild = 2
}