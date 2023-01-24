using System.Text.Json.Serialization;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-structure" />
public sealed class IdentifyEvent : GatewayEvent
{
	/// <summary>
	/// Authentication token
	/// </summary>
	[JsonPropertyName("token")]
	public string Token { get; set; }

	/// <see href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-connection-properties" />
	[JsonPropertyName("properties")]
	public ConnectionProperties Properties { get; set; } = null!;

	/// <summary>
	/// Whether this connection supports compression of packets
	/// </summary>
	[JsonPropertyName("compress")]
	public bool? Compress { get; set; }

	/// <summary>
	/// Value between 50 and 250,
	/// total number of members where the gateway will stop sending offline members in the guild member list
	/// </summary>
	[JsonPropertyName("large_treshold")]
	public int? LargeTreshold { get; set; }

	/// <summary>
	/// Used for Guild Sharding
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sharding" />
	[JsonPropertyName("shard")]
	public (int, int)? Shard { get; set; }

	// TODO: presence field.

	/// <summary>
	/// Gateway Intents you wish to receive
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#gateway-intents" />
	[JsonPropertyName("intents")]
	public int Intents { get; set; }

	public sealed class ConnectionProperties
	{
		[JsonPropertyName("os")]
		public string OperatingSystem { get; set; } = null!;

		[JsonPropertyName("browser")]
		public string Browser { get; set; } = null!;

		[JsonPropertyName("device")]
		public string Device { get; set; } = null!;
	}
}