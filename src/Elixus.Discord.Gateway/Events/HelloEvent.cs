using System.Text.Json.Serialization;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway#hello-event" />
/// <seealso href="https://discord.com/developers/docs/topics/gateway#sending-heartbeats"/>
internal class HelloEvent : GatewayEvent
{
	[JsonPropertyName("heartbeat_interval")]
	public int HeartbeatInterval { get; set; }
}
