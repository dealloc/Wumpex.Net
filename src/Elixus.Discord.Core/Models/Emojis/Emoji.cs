using System.Text.Json.Serialization;
using Elixus.Discord.Core.Models.Permissions;
using Elixus.Discord.Core.Models.Users;

namespace Elixus.Discord.Core.Models.Emojis;

/// <see href="https://discord.com/developers/docs/resources/emoji#emoji-object" />
public sealed class Emoji
{
	/// <summary>
	/// emoji id
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// emoji name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// roles allowed to use this emoji.
	/// </summary>
	[JsonPropertyName("roles")]
	public List<Role> Roles { get; set; } = new(0);

	/// <summary>
	/// user that created this emoji.
	/// </summary>
	[JsonPropertyName("user")]
	public User? User { get; set; }

	/// <summary>
	/// whether this emoji must be wrapped in colons.
	/// </summary>
	[JsonPropertyName("require_colons")]
	public bool? RequireColons { get; set; }

	/// <summary>
	/// Whether this emoji is managed.
	/// </summary>
	[JsonPropertyName("managed")]
	public bool? Managed { get; set; }

	/// <summary>
	/// whether this emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public bool? Animated { get; set; }

	/// <summary>
	/// whether this emoji can be used, may be false due to loss of Server Boosts.
	/// </summary>
	[JsonPropertyName("available")]
	public bool? Available { get; set; }
}