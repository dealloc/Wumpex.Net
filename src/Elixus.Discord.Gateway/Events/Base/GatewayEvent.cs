using System.Text.Json.Serialization;
using Elixus.Discord.Gateway.Constants;

namespace Elixus.Discord.Gateway.Events.Base;

/// <summary>
/// Base class for gateway events.
/// </summary>
public abstract class GatewayEvent
{
	//
}

/// <summary>
/// Wrapper for gateway events in the format that Discord expects to receive them from us.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
public class GatewayEvent<TEvent> where TEvent : GatewayEvent
{
	/// <summary>
	/// Gateway opcode, which indicates the payload type
	/// </summary>
	[JsonPropertyName("op")]
	public GatewayOpcodes Opcode { get; set; }

	/// <summary>
	/// Event data
	/// </summary>
	[JsonPropertyName("d")]
	public TEvent Event { get; set; } = default!;
}