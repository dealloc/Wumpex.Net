using System.Net.WebSockets;
using Wumpex.Net.Core.Exceptions;
using Wumpex.Net.Gateway.Constants;

namespace Wumpex.Net.Gateway.Exceptions;

/// <summary>
/// Thrown when the Discord WS Gateway closed.
/// </summary>
public sealed class GatewayClosedException : DiscordException
{
	/// <summary>
	/// Gets the opcode used to close the connection.
	/// Note that the value may also be a <see cref="WebSocketCloseStatus" />.
	/// </summary>
	public GatewayCloseCodes CloseCode { get; }

	/// <summary>
	/// Creates a <see cref="GatewayClosedException" /> using the given <paramref name="socket" /> as source.
	/// </summary>
	public GatewayClosedException(WebSocket socket) : base(socket.CloseStatusDescription ?? "Websocket closed without description")
	{
		CloseCode = socket switch
		{
			_ when socket.CloseStatus is not null => (GatewayCloseCodes)socket.CloseStatus,
			_ => (GatewayCloseCodes)WebSocketCloseStatus.Empty,
		};

		CanRecover = CloseCode switch
		{
			GatewayCloseCodes.UnknownError => true,
			GatewayCloseCodes.UnknownOpcode => true,
			GatewayCloseCodes.DecodeError => true,
			GatewayCloseCodes.NotAuthenticated => true,
			GatewayCloseCodes.AuthenticationFailed => false,
			GatewayCloseCodes.AlreadyAuthenticated => true,
			GatewayCloseCodes.InvalidSequence => true,
			GatewayCloseCodes.RateLimited => true,
			GatewayCloseCodes.SessionTimedOut => true,
			GatewayCloseCodes.InvalidShard => false,
			GatewayCloseCodes.ShardingRequired => false,
			GatewayCloseCodes.InvalidApiVersion => false,
			GatewayCloseCodes.InvalidIntent => false,
			GatewayCloseCodes.DisallowedIntent => false,
			_ => false
		};
	}
}