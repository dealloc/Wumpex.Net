using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Gateway;

/// <summary>
/// Active sessions are indicated with an "online", "idle", or "dnd" string per platform.
/// If a user is offline or invisible, the corresponding field is not present.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#client-status-object" />
public sealed class ClientStatus
{
	/// <summary>
	/// User's status set for an active desktop (Windows, Linux, Mac) application session.
	/// </summary>
	[JsonPropertyName("desktop")]
	public string? Desktop { get; set; }

	/// <summary>
	/// User's status set for an active mobile (iOS, Android) application session.
	/// </summary>
	[JsonPropertyName("mobile")]
	public string? Mobile { get; set; }

	/// <summary>
	/// User's status set for an active web (browser, bot account) application session.
	/// </summary>
	[JsonPropertyName("web")]
	public string? Web { get; set; }
}