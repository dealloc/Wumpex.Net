using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Models.Gateway;

/// <summary>
/// A user's presence is their current state on a guild. This event is sent when a user's presence or info, such as name or avatar, is updated.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#presence-update" />
public sealed class PresenceUpdate
{
	/// <summary>
	/// User whose presence is being updated.
	/// </summary>
	[JsonPropertyName("user")]
	public User User { get; set; } = null!;

	/// <summary>
	/// ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; } = null!;

	/// <summary>
	/// Either "idle", "dnd", "online", or "offline".
	/// </summary>
	[JsonPropertyName("status")]
	public string Status { get; set; } = null!;

	/// <summary>
	/// User's current activities.
	/// </summary>
	[JsonPropertyName("activities")]
	public List<Activity> Activities = new(0);

	/// <summary>
	/// User's platform-dependent status.
	/// </summary>
	[JsonPropertyName("client_status")]
	public ClientStatus ClientStatus { get; set; } = null!;
}