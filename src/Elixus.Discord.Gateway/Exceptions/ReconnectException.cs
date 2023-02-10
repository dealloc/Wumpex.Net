using Elixus.Discord.Core.Exceptions;
using Elixus.Discord.Gateway.Events;

namespace Elixus.Discord.Gateway.Exceptions;

/// <summary>
/// Thrown in response to a <see cref="ReconnectEvent" />
/// </summary>
public class ReconnectException : DiscordException
{
	/// <inheritdoc cref="DiscordException.CanRecover" />
	public override bool? CanRecover { get; init; } = true;

	/// <inheritdoc cref="DiscordException()" />
	public ReconnectException() : base("Discord requested the gateway to disconnect and reconnect")
	{
		//
	}
}