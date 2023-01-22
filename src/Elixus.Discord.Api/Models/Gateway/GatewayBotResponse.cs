using System.Text.Json.Serialization;

namespace Elixus.Discord.Api.Models.Gateway;

/// <summary>
/// Represents the response from the Get Gateway Bot API call.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway#get-gateway-bot" />
public sealed class GatewayBotResponse
{
	/// <summary>
	/// WSS URL that can be used for connecting to the Gateway
	/// </summary>
	public Uri Url { get; init; } = null!;

	/// <summary>
	/// Recommended number of shards to use when connecting
	/// </summary>
	public int Shards { get; init; }

	/// <summary>
	/// Information on the current session start limit
	/// </summary>
	[JsonPropertyName("session_start_limit")]
	public SessionStartLimit SessionStartLimit { get; init; } = null!;
}
