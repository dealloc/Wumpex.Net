using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Channels;
using Elixus.Discord.Core.Models.Guilds;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;
using Elixus.Discord.Core.Models.Users;

namespace Elixus.Discord.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure" />
public class Interaction
{
	/// <summary>
	/// ID of the interaction.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// ID of the application this interaction is for.
	/// </summary>
	[JsonPropertyName("application_id")]
	public string ApplicationId { get; set; } = null!;

	/// <summary>
	/// Type of interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public InteractionType Type { get; set; }

	/// <summary>
	/// Interaction data payload.
	/// </summary>
	[JsonPropertyName("data")]
	public ApplicationCommandData? Data { get; set; }

	/// <summary>
	/// Guild that the interaction was sent from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// Channel that the interaction was sent from.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string? ChannelId { get; set; }

	/// <summary>
	/// Guild member data for the invoking user, including permissions.
	/// </summary>
	/// <remarks>
	/// <see cref="Member" /> is sent when the interaction is invoked in a guild, and <see cref="User" /> is sent when invoked in a DM.
	/// </remarks>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }

	/// <summary>
	/// User object for the invoking user, if invoked in a DM.
	/// </summary>
	/// <remarks>
	/// <see cref="Member" /> is sent when the interaction is invoked in a guild, and <see cref="User" /> is sent when invoked in a DM.
	/// </remarks>
	[JsonPropertyName("user")]
	public User? User { get; set; }

	/// <summary>
	/// Continuation token for responding to the interaction.
	/// </summary>
	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	/// <summary>
	/// Read-only property, always 1.
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; }

	/// <summary>
	/// For components, the message they were attached to.
	/// </summary>
	[JsonPropertyName("message")]
	public Message? Message { get; set; }

	/// <summary>
	/// Bitwise set of permissions the app or bot has within the channel the interaction was sent from.
	/// </summary>
	[JsonPropertyName("app_permissions")]
	public string? AppPermissions { get; set; }

	/// <summary>
	/// Selected language of the invoking user.
	/// </summary>
	/// <remarks>
	/// This is available on all interaction types except PING
	/// </remarks>
	/// <seealso href="https://discord.com/developers/docs/reference#locales" />
	[JsonPropertyName("locale")]
	public string? Locale { get; set; }

	/// <summary>
	/// Guild's preferred locale, if invoked in a guild.
	/// </summary>
	[JsonPropertyName("guild_locale")]
	public string GuildLocale { get; set; } = null!;
}