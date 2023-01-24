using System.Text.Json.Serialization;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway#hello-event" />
/// <seealso href="https://discord.com/developers/docs/topics/gateway#sending-heartbeats"/>
public class HelloEvent : GatewayEvent
{
	/// <summary>
	/// The interval (in milliseconds) at which heartbeats should be sent.
	/// </summary>
	[JsonPropertyName("heartbeat_interval")]
	public int HeartbeatInterval { get; set; }
}
