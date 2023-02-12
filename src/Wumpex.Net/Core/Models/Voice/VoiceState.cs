using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Guilds;

namespace Wumpex.Net.Core.Models.Voice;

/// <summary>
/// Used to represent a user's voice connection status.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/voice#voice-state-object" />
public sealed class VoiceState
{
	/// <summary>
	/// The guild id this voice state is for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// The channel id this user is connected to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string? ChannelId { get; set; }

	/// <summary>
	/// The user id this voice state is for.
	/// </summary>
	[JsonPropertyName("user_id")]
	public string UserId { get; set; } = null!;

	/// <summary>
	/// The guild member this voice state is for.
	/// </summary>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }

	/// <summary>
	/// The session id for this voice state.
	/// </summary>
	[JsonPropertyName("session_id")]
	public string SessionId { get; set; } = null!;

	/// <summary>
	/// whether this user is deafened by the server.
	/// </summary>
	[JsonPropertyName("deaf")]
	public bool Deaf { get; set; }

	/// <summary>
	/// Whether this user is muted by the server.
	/// </summary>
	[JsonPropertyName("mute")]
	public bool Mute { get; set; }

	/// <summary>
	/// whether this user is locally deafened.
	/// </summary>
	[JsonPropertyName("self_deaf")]
	public bool SelfDeaf { get; set; }

	/// <summary>
	/// whether this user is locally muted.
	/// </summary>
	[JsonPropertyName("self_mute")]
	public bool SelfMute { get; set; }

	/// <summary>
	/// Whether this user is streaming using "Go Live".
	/// </summary>
	[JsonPropertyName("self_stream")]
	public bool? SelfStream { get; set; }

	/// <summary>
	/// whether this user's camera is enabled.
	/// </summary>
	[JsonPropertyName("self_video")]
	public bool SelfVideo { get; set; }

	/// <summary>
	/// Whether this user's permission to speak is denied.
	/// </summary>
	[JsonPropertyName("suppress")]
	public bool Suppress { get; set; }

	/// <summary>
	/// The time at which the user requested to speak.
	/// </summary>
	[JsonPropertyName("request_to_speak_timestamp")]
	public DateTime? RequestToSpeakTimestamp { get; set; }
}