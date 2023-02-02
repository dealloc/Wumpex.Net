using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Handlers.Events;

/// <summary>
/// Handles the <see cref="ReconnectEvent" />.
/// This handler will attempt to stop the gateway and force a resume.
/// </summary>
internal sealed class ReconnectEventHandler : IEventHandler<ReconnectEvent>
{
	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public ValueTask HandleEvent(ReconnectEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}