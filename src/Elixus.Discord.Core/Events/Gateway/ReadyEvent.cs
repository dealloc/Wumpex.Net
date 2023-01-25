using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Events.Gateway;

/// <summary>
/// The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions).
/// The ready event can be the largest and most complex event the gateway will send, as it contains all the state required for a client to begin interacting with the rest of the platform.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#ready" />
public class ReadyEvent
{
	/// <summary>
	/// API version
	/// </summary>
	/// <see href="https://discord.com/developers/docs/reference#api-versioning-api-versions" />
	[JsonPropertyName("v")]
	public int ApiVersion { get; set; }
}