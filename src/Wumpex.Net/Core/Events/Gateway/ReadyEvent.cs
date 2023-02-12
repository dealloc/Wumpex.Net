using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Applications;
using Wumpex.Net.Core.Models.Guilds;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Events.Gateway;

/// <summary>
/// The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions).
/// The ready event can be the largest and most complex event the gateway will send, as it contains all the state required for a client to begin interacting with the rest of the platform.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#ready" />
[Intent(GatewayIntents.Default)]
public class ReadyEvent
{
	/// <summary>
	/// API version
	/// </summary>
	/// <see href="https://discord.com/developers/docs/reference#api-versioning-api-versions" />
	[JsonPropertyName("v")]
	public int ApiVersion { get; set; }

	/// <summary>
	/// Information about the user including email
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/user#user-object" />
	[JsonPropertyName("user")]
	public User User { get; set; } = null!;

	/// <summary>
	/// Guilds the user is in
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/guild#unavailable-guild-object" />
	[JsonPropertyName("guilds")]
	public List<UnavailableGuild> Guilds { get; set; } = new(0);

	/// <summary>
	/// Used for resuming connections
	/// </summary>
	[JsonPropertyName("session_id")]
	public string SessionId { get; set; } = null!;

	/// <summary>
	/// Gateway URL for resuming connections
	/// </summary>
	[JsonPropertyName("resume_gateway_url")]
	public string ResumeGatewayUrl { get; set; } = null!;

	/// <summary>
	/// Shard information associated with this session, if sent when identifying
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sharding"/>
	[JsonPropertyName("shard")]
	public (int, int)? Shard { get; set; }

	/// <summary>
	/// Partial application object.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#ready-ready-event-fields" />
	[JsonPropertyName("application")]
	public PartialApplication Application { get; set; } = null!;
}