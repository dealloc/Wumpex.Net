using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;
using Wumpex.Net.Gateway.Exceptions;

namespace Wumpex.Net.Gateway.Handlers.Events;

/// <summary>
/// Handles the <see cref="ReconnectEvent" />.
/// This handler will attempt to stop the gateway and force a resume.
/// </summary>
internal sealed class ReconnectEventHandler : IEventHandler<ReconnectEvent>
{
	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public ValueTask HandleEvent(ReconnectEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		throw new ReconnectException();
	}
}