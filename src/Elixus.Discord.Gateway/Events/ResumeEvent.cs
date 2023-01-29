using System.Text.Json.Serialization;

namespace Elixus.Discord.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#resume" />
public sealed class ResumeEvent
{
	/// <summary>
	/// Session token.
	/// </summary>
	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	/// <summary>
	/// Session ID.
	/// </summary>
	[JsonPropertyName("session_id")]
	public string SessionId { get; set; } = null!;

	/// <summary>
	/// Last sequence number received.
	/// </summary>
	[JsonPropertyName("seq")]
	public int Sequence { get; set; }
}