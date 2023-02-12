using System.Text.Json.Serialization;
using Wumpex.Net.Gateway.Constants;

namespace Wumpex.Net.Gateway.Events.Base;

/// <summary>
/// Wrapper for gateway events in the format that Discord expects to receive them from us.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#payload-structure" />
internal class GatewayEvent<TEvent> where TEvent : class
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