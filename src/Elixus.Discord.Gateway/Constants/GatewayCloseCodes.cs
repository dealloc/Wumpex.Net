namespace Elixus.Discord.Gateway.Constants;

/// <summary>
/// In order to prevent broken reconnect loops, you should consider some close codes as a signal to stop reconnecting.
/// This can be because your token expired, or your identification is invalid.
/// This table explains what the application defined close codes for the gateway are, and which close codes you should not attempt to reconnect.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/opcodes-and-status-codes" />
public enum GatewayCloseCodes
{
	/// <summary>
	/// We're not sure what went wrong. Try reconnecting?
	/// </summary>
	/// <remarks>Reconnect</remarks>
	UnknownError = 4000,

	/// <summary>
	/// You sent an invalid Gateway opcode or an invalid payload for an opcode. Don't do that!
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes" />
	/// <remarks>Reconnect</remarks>
	UnknownOpcode = 4001,

	/// <summary>
	/// You sent an invalid payload to Discord. Don't do that!
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sending-events" />
	/// <remarks>Reconnect</remarks>
	DecodeError = 4002,

	/// <summary>
	/// You sent us a payload prior to identifying.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#identifying" />
	/// <remarks>Reconnect</remarks>
	NotAuthenticated = 4003,

	/// <summary>
	/// The account token sent with your identify payload is incorrect.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#identify" />
	/// <remarks>Cannot reconnect</remarks>
	AuthenticationFailed = 4004,

	/// <summary>
	/// You sent more than one identify payload. Don't do that!
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#identifying" />
	/// <remarks>Reconnect</remarks>
	AlreadyAuthenticated = 4005,

	/// <summary>
	/// The sequence sent when resuming the session was invalid. Reconnect and start a new session.
	/// </summary>
	/// <remarks>Reconnect</remarks>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#resume" />
	InvalidSequence = 4007,

	/// <summary>
	/// Woah nelly! You're sending payloads to us too quickly. Slow it down! You will be disconnected on receiving this.
	/// </summary>
	/// <remarks>Reconnect</remarks>
	RateLimited = 4008,

	/// <summary>
	/// Your session timed out. Reconnect and start a new one.
	/// </summary>
	/// <remarks>Reconnect</remarks>
	SessionTimedOut = 4009,

	/// <summary>
	/// You sent us an invalid shard when identifying.
	/// </summary>
	/// <remarks>Cannot reconnect</remarks>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sharding" />
	InvalidShard = 4010,

	/// <summary>
	/// The session would have handled too many guilds - you are required to shard your connection in order to connect.
	/// </summary>
	/// <remarks>Cannot reconnect</remarks>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#sharding" />
	ShardingRequired = 4011,

	/// <summary>
	/// You sent an invalid version for the gateway.
	/// </summary>
	/// <remarks>Cannot reconnect</remarks>
	InvalidApiVersion = 4012,

	/// <summary>
	/// You sent an invalid intent for a Gateway Intent.
	/// You may have incorrectly calculated the bitwise value.
	/// </summary>
	/// <remarks>Cannot reconnect</remarks>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#gateway-intents" />
	InvalidIntent = 4013,

	/// <summary>
	/// You sent a disallowed intent for a Gateway Intent.
	/// You may have tried to specify an intent that you have not enabled or are not approved for.
	/// </summary>
	/// <remarks>Cannot reconnect</remarks>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#gateway-intents" />
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#privileged-intents" />
	DisallowedIntent = 4014
}