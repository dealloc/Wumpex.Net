using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;

namespace Wumpex.Net.Core.Events.Voice;

/// <summary>
/// Sent when a guild's voice server is updated.
/// This is sent when initially connecting to voice, and when the current voice instance fails over to a new server.
/// </summary>
[Intent(GatewayIntents.GuildVoiceStates)]
public sealed class VoiceServerUpdateEvent
{
	/// <summary>
	/// Voice connection token.
	/// </summary>
	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	/// <summary>
	/// Guild this voice server update is for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; } = null!;

	/// <summary>
	/// Voice server host.
	/// </summary>
	[JsonPropertyName("endpoint")]
	public string? Endpoint { get; set; }
}