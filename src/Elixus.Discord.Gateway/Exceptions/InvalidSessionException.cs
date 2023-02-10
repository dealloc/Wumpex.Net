using Elixus.Discord.Core.Exceptions;
using Elixus.Discord.Gateway.Events;

namespace Elixus.Discord.Gateway.Exceptions;

/// <summary>
/// Thrown in response to <see cref="InvalidSessionEvent" />.
/// </summary>
public class InvalidSessionException : DiscordException
{
	/// <inheritdoc cref="DiscordException.CanRecover" />
	public override bool? CanRecover { get; init; } = false;

	/// <inheritdoc cref="DiscordException()" />
	public InvalidSessionException() : base("Gateway received an invalid session")
	{
		//
	}
}