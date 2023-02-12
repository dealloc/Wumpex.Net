namespace Wumpex.Net.Core.Exceptions;

/// <summary>
/// Base <see cref="Exception" /> class for exceptions thrown within the library.
/// </summary>
public abstract class DiscordException : Exception
{
	/// <summary>
	/// Whether or not the error is recoverable.
	/// </summary>
	/// <remarks>
	/// Whether or not an error is recoverable depends on the type of error.
	/// Some errors have no notion of being recoverable, in which case this will be <c>null</c>.
	/// The parent context (like Discord WS Gateway) will check if an error is able to resume.
	/// </remarks>
	public virtual bool? CanRecover { get; init; }

	/// <inheritdoc cref="Exception()" />
	protected DiscordException()
	{
		//
	}

	/// <inheritdoc cref="Exception(string?)" />
	protected DiscordException(string? message) : base(message)
	{
		//
	}

	/// <inheritdoc cref="Exception(string?, Exception?)" />
	protected DiscordException(string? message, Exception? innerException) : base(message, innerException)
	{
		//
	}
}