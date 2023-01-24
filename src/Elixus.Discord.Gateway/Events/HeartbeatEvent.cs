using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#heartbeat" />
internal sealed class HeartbeatEvent : GatewayEvent
{
	public int? Sequence { get; set; }
}