namespace Wumpex.Net.Gateway.Constants;

/// <see href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes"/>
public enum GatewayOpcodes
{
	/// <summary>
	/// An event was dispatched.
	/// </summary>
	Dispatch = 0,

	/// <summary>
	/// Fired periodically by the client to keep the connection alive.
	/// </summary>
	Heartbeat = 1,

	/// <summary>
	/// Starts a new session during the initial handshake.
	/// </summary>
	Identify = 2,

	/// <summary>
	/// Update the client's presence.
	/// </summary>
	PresenceUpdate = 3,

	/// <summary>
	/// Used to join/leave or move between voice channels.
	/// </summary>
	VoiceStateUpdate = 4,

	/// <summary>
	/// Resume a previous session that was disconnected.
	/// </summary>
	Resume = 6,

	/// <summary>
	/// You should attempt to reconnect and resume immediately.
	/// </summary>
	Reconnect = 7,

	/// <summary>
	/// Request information about offline guild members in a large guild.
	/// </summary>
	RequestGuildMembers = 8,

	/// <summary>
	/// The session has been invalidated. You should reconnect and identify/resume accordingly.
	/// </summary>
	InvalidSession = 9,

	/// <summary>
	/// Sent immediately after connecting, contains the heartbeat_interval to use.
	/// </summary>
	Hello = 10,

	/// <summary>
	/// Sent in response to receiving a heartbeat to acknowledge that it has been received.
	/// </summary>
	HeartbeatAck = 11
}