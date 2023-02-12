namespace Wumpex.Net.Gateway.Events;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#heartbeat" />
public sealed class HeartbeatEvent
{
	/// <summary>
	/// The last received sequence from the gateway.
	/// </summary>
	/// <remarks>This field is not automatically serialized, but manually.</remarks>
	public int? Sequence { get; set; }
}