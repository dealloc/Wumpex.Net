namespace Elixus.Discord.Gateway.Contracts.Events;

/// <summary>
/// Handles <typeparamref name="TEvent" /> received by the Discord WS Gateway.
/// </summary>
internal interface IEventHandler<in TEvent> where TEvent : class
{
	/// <summary>
	/// Handles the <paramref name="event" />.
	/// </summary>
	ValueTask HandleEvent(TEvent @event, CancellationToken cancellationToken);
}