using System.Text.Json;
using Wumpex.Net.Core.Exceptions;

namespace Wumpex.Net.Gateway.Exceptions;

/// <summary>
/// An <see cref="Exception" /> thrown when the gateway fails to parse an incoming message.
/// </summary>
public sealed class GatewayParserException : DiscordException
{
	/// <inheritdoc cref="DiscordException.CanRecover" />
	public override bool? CanRecover => false;

	/// <summary>
	/// Creates a new instance of <see cref="GatewayParserException" />.
	/// </summary>
	/// <param name="expected">The expected JSON token type.</param>
	/// <param name="token">The actually encountered JSON token type.</param>
	/// <param name="position">The position at which the invalid JSON token was encountered.</param>
	public GatewayParserException(JsonTokenType expected, JsonTokenType token, long position) : base($"Unexpected JSON token {Enum.GetName(token)} at {position}, expected {Enum.GetName(expected)}")
	{
		//
	}
}
