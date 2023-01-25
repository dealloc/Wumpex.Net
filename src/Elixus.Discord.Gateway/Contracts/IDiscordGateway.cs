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
	/// Starts the <see cref="IDiscordGateway" /> and run it until the <paramref name="cancellationToken" /> is set.
	/// </summary>
	/// <param name="endpoint">The websocket gateway to connect to.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to stop the server.</param>
	Task RunAsync(Uri endpoint, CancellationToken cancellationToken = default);

	/// <summary>
	/// Sends the given <typeparamref name="TEvent" /> over the gateway.
	/// </summary>
	// TODO: this method needs to be refactored OUT of the gateway contract to avoid cyclic dependencies.
	Task SendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, new();
}