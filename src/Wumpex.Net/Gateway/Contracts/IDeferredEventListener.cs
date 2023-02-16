using Wumpex.Net.Gateway.Contracts.Events;

namespace Wumpex.Net.Gateway.Contracts;

/// <summary>
/// Allows capturing events based on a given predicate.
/// This allows you to one-off listen for specific events (like the Voice server does when connecting).
/// </summary>
/// <remarks>When registering in the container make sure to also register this as an <see cref="IEventHandler{TEvent}" /></remarks>
public interface IDeferredEventListener<TEvent> : IEventHandler<TEvent> where TEvent : class
{
	/// <summary>
	/// Waits for a <typeparamref name="TEvent" /> that matches <paramref name="predicate" />.
	/// </summary>
	Task<TEvent> WaitFor(Func<TEvent, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default);
}