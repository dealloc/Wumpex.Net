using Elixus.Discord.Gateway.Constants;

namespace Elixus.Discord.Gateway.Events.Base;

/// <summary>
/// Contextual information about the actual event payload received from Discord WS Gateway.
/// This data type contains the opcode, event sequence and event name.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
public sealed record EventContext
{
	/// <summary>
	/// Gateway opcode, which indicates the payload type
	/// </summary>
	/// <see href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes"/>
	public GatewayOpcodes Opcode { get; set; }

	/// <summary>
	/// Sequence number of event used for resuming sessions and heartbeating
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#resuming"/>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sending-heartbeats"/>
	public int? Sequence { get; set; }

	/// <summary>
	/// Event name
	/// </summary>
	public string? EventName { get; set; }
}
