using System.Diagnostics;
using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Guilds;
using Elixus.Discord.Core.Models.Users;

namespace Elixus.Discord.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-member-object" />
[DebuggerDisplay("{User}")]
public sealed class GuildMember
{
	/// <summary>
	/// The user this guild member represents.
	/// </summary>
	[JsonPropertyName("user")]
	public User? User { get; set; }

	/// <summary>
	/// This user's guild nickname.
	/// </summary>
	[JsonPropertyName("nick")]
	public string? Nickname { get; set; }

	/// <summary>
	/// The member's guild avatar hash.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	/// <summary>
	/// Array of role object ids.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/permissions#role-object" />
	[JsonPropertyName("roles")]
	public List<string> Roles { get; set; } = new(0);

	/// <summary>
	/// When the user joined the guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public DateTime JoinedAt { get; set; }

	/// <summary>
	/// When the user started boosting the guild.
	/// </summary>
	[JsonPropertyName("premium_since")]
	public DateTime? PremiumSince { get; set; }

	/// <summary>
	/// Whether the user is deafened in voice channels.
	/// </summary>
	[JsonPropertyName("deaf")]
	public bool Deaf { get; set; }

	/// <summary>
	/// Whether the user is muted in voice channels.
	/// </summary>
	[JsonPropertyName("mute")]
	public bool Mute { get; set; }

	/// <summary>
	/// Guild member flags represented as a bit set, defaults to 0
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-flags" />
	[JsonPropertyName("flags")]
	public GuildMemberFlags Flags { get; set; } = 0;

	/// <summary>
	/// Whether the user has not yet passed the guild's Membership Screening requirements.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/guild#membership-screening-object" />
	[JsonPropertyName("pending")]
	public bool? Pending {get; set; }

	/// <summary>
	/// total permissions of the member in the channel, including overwrites, returned when in the interaction object.
	/// </summary>
	[JsonPropertyName("permissions")]
	public string? Permissions { get; set; }

	/// <summary>
	/// When the user's timeout will expire and the user will be able to communicate in the guild again, null or a time in the past if the user is not timed out.
	/// </summary>
	[JsonPropertyName("communication_disabled_until")]
	public DateTime? CommunicationDisabledUntil { get; set; }
}