using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Contracts.Events;

/// <summary>
/// Handles <typeparamref name="TEvent" /> received by the Discord WS Gateway.
/// </summary>
public interface IEventHandler<in TEvent> where TEvent : class
{
	/// <summary>
	/// Handles the <paramref name="event" />.
	/// </summary>
	ValueTask HandleEvent(TEvent @event, EventContext context, CancellationToken cancellationToken);
}