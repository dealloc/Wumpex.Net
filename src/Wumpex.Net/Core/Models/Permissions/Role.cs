using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Permissions;

/// <summary>
/// Roles represent a set of permissions attached to a group of users. Roles have names, colors, and can be "pinned" to the side bar, causing their members to be listed separately. Roles can have separate permission profiles for the global context (guild) and channel context.
/// The @everyone role has the same ID as the guild it belongs to.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/permissions#role-object" />
public sealed class Role
{
	/// <summary>
	/// role id.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// role name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// integer representation of hexadecimal color code.
	/// </summary>
	[JsonPropertyName("color")]
	public int Color { get; set; }

	/// <summary>
	/// if this role is pinned in the user listing.
	/// </summary>
	[JsonPropertyName("hoist")]
	public bool Hoist { get; set; }

	/// <summary>
	/// role icon hash.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("icon")]
	public string? Icon { get; set; }

	/// <summary>
	/// role unicode emoji.
	/// </summary>
	[JsonPropertyName("unicode_emoji")]
	public string? UnicodeEmoji { get; set; }

	/// <summary>
	/// position of this role.
	/// </summary>
	[JsonPropertyName("position")]
	public int Position { get; set; }

	/// <summary>
	/// permission bit set.
	/// </summary>
	[JsonPropertyName("permissions_new")]
	public string Permissions { get; set; } = null!;

	/// <summary>
	/// whether this role is managed by an integration.
	/// </summary>
	[JsonPropertyName("managed")]
	public bool Managed { get; set; }

	/// <summary>
	/// whether this role is mentionable.
	/// </summary>
	[JsonPropertyName("mentionable")]
	public bool Mentionable { get; set; }

	/// <summary>
	/// The tags this role has.
	/// </summary>
	[JsonPropertyName("tags")]
	public RoleTag Tags { get; set; } = null!;
}