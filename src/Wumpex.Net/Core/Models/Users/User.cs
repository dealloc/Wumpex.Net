using System.Diagnostics;
using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Users;

namespace Wumpex.Net.Core.Models.Users;

/// <see href="https://discord.com/developers/docs/resources/user#user-object" />
[DebuggerDisplay("{Username}#{Discriminator}")]
public sealed class User
{
	/// <summary>
	/// the user's id
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The user's username, not unique across the platform
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("username")]
	public string Username { get; set; } = null!;

	/// <summary>
	/// The user's 4-digit discord-tag
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("discriminator")]
	public string Discriminator { get; set; } = null!;

	/// <summary>
	/// The user's avatar hash
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; } = null!;

	/// <summary>
	/// Whether the user belongs to an OAuth2 application
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("bot")]
	public bool? Bot { get; set; }

	/// <summary>
	/// Whether the user is an Official Discord System user (part of the urgent message system)
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("system")]
	public bool? System { get; set; }

	/// <summary>
	/// Whether the user has two factor enabled on their account
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("mfa_enabled")]
	public bool? MfaEnabled { get; set; }

	/// <summary>
	/// The user's banner hash
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("banner")]
	public string? Banner { get; set; } = null!;

	/// <summary>
	/// The user's banner color encoded as an integer representation of hexadecimal color code
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("accent_color")]
	public int? AccentColor { get; set; }

	/// <summary>
	/// The user's chosen language option
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	[JsonPropertyName("locale")]
	public string? Locale { get; set; }

	/// <summary>
	/// Whether the email on this account has been verified
	/// </summary>
	/// <remarks>Requires email scope</remarks>
	[JsonPropertyName("verified")]
	public bool? Verified { get; set; }

	/// <summary>
	/// The user's email
	/// </summary>
	/// <remarks>Requires email scope</remarks>
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	/// <summary>
	/// The flags on a user's account
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	/// <seealso href="https://discord.com/developers/docs/resources/user#user-object-user-flags" />
	[JsonPropertyName("flags")]
	public UserFlags? Flags { get; set; }

	/// <summary>
	/// The type of Nitro subscription on a user's account
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	/// <seealso href="https://discord.com/developers/docs/resources/user#user-object-premium-types" />
	[JsonPropertyName("premium_type")]
	public PremiumTypes? PremiumType { get; set; }

	/// <summary>
	/// The public flags on a user's account
	/// </summary>
	/// <remarks>Requires identify scope</remarks>
	/// <seealso href="https://discord.com/developers/docs/resources/user#user-object-user-flags" />
	[JsonPropertyName("public_flags")]
	public UserFlags? PublicFlags { get; set; }
}
