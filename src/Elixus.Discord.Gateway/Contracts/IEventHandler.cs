using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Contracts;

/// <summary>
/// Handles <typeparamref name="TEvent" /> received by the Discord WS Gateway.
/// </summary>
internal interface IEventHandler<in TEvent> where TEvent : GatewayEvent
{
	/// <summary>
	/// Handles the <paramref name="event" />.
	/// </summary>
	ValueTask HandleEvent(TEvent @event, CancellationToken cancellationToken);
}