using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Events.Handlers;

/// <summary>
/// Handles the <see cref="InvalidSessionEvent" />.
/// This handler will stop the gateway and resume if the event allows it.
/// </summary>
internal sealed class InvalidSessionEventHandler : IEventHandler<InvalidSessionEvent>
{
	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public ValueTask HandleEvent(InvalidSessionEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}