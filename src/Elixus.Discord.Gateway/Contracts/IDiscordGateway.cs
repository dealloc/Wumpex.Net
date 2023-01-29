namespace Elixus.Discord.Gateway.Contracts;

/// <summary>
/// Provides interactions with the Discord WS Gateway.
/// </summary>
/// <remarks>
/// You normally shouldn't need to access this directly.
/// </remarks>
public interface IDiscordGateway
{
	/// <summary>
	/// Whether or not the gateway is currently capable of recovering if required.
	/// This should (usually) be <c>true</c> after <see cref="ConfigureReconnect" /> was called.
	/// </summary>
	ValueTask<bool> CanRecover { get; }

	/// <summary>
	/// Gets the endpoint at which the gateway should resume (if available).
	/// </summary>
	/// <see cref="ConfigureReconnect" />
	ValueTask<Uri?> ResumeEndpoint { get; }

	/// <summary>
	/// Gets the session to use when resuming (if available).
	/// </summary>
	/// <see cref="ConfigureReconnect" />
	ValueTask<string?> ResumeSession { get; }

	/// <summary>
	/// Starts the <see cref="IDiscordGateway" /> and run it until the <paramref name="cancellationToken" /> is set.
	/// </summary>
	/// <param name="endpoint">The websocket gateway to connect to.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to stop the server.</param>
	Task RunAsync(Uri endpoint, CancellationToken cancellationToken = default);

	/// <summary>
	/// Sends the given <typeparamref name="TEvent" /> over the gateway.
	/// </summary>
	Task SendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, new();

	/// <summary>
	/// Configures the information required for reconnecting.
	/// </summary>
	/// <param name="endpoint">The endpoint to use when reconnecting.</param>
	/// <param name="session">The session ID to use when reconnecting.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to cancel the operation.</param>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#resuming" />
	ValueTask ConfigureReconnect(Uri endpoint, string session, CancellationToken cancellationToken = default);
}