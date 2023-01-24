using Elixus.Discord.Gateway.Constants;

namespace Elixus.Discord.Gateway.Events.Base;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
internal ref struct EventPayload
{
	/// <summary>
	/// Gateway opcode, which indicates the payload type
	/// </summary>
	/// <see href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes"/>
	public GatewayOpcodes Opcode { get; set; }

	/// <summary>
	/// The binary representation of the event data.
	/// </summary>
	public ReadOnlySpan<byte> EventData { get; set; }

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
