using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;
using Wumpex.Net.Gateway.Exceptions;

namespace Wumpex.Net.Gateway.Handlers.Events;

/// <summary>
/// Handles the <see cref="InvalidSessionEvent" />.
/// This handler will stop the gateway and resume if the event allows it.
/// </summary>
internal sealed class InvalidSessionEventHandler : IEventHandler<InvalidSessionEvent>
{
	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public ValueTask HandleEvent(InvalidSessionEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		throw new InvalidSessionException();
	}
}