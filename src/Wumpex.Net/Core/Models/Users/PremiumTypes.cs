namespace Wumpex.Net.Core.Models.Users;

/// <summary>
/// Premium types denote the level of premium a user has.
/// Visit the Nitro page to learn more about the premium plans we currently offer.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/user#user-object-premium-types" />
public enum PremiumTypes
{
	/// <summary>
	/// None
	/// </summary>
	None = 0,

	/// <summary>
	/// Nitro classic
	/// </summary>
	NitroClassic = 1,

	/// <summary>
	/// Nitro
	/// </summary>
	Nitro = 2,

	/// <summary>
	/// Nitro Basic
	/// </summary>
	NitroBasic = 3
}