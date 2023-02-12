using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events;

/// <summary>
/// Handles <see cref="HeartbeatAckEvent" />.
/// This handler will notify the <see cref="IHeartbeatService" /> that an ACK was received.
/// </summary>
/// <seealso cref="IHeartbeatService" />
internal sealed class HeartbeatAckEventHandler : IEventHandler<HeartbeatAckEvent>
{
	private readonly IHeartbeatService _heartbeatService;

	public HeartbeatAckEventHandler(IHeartbeatService heartbeatService)
	{
		_heartbeatService = heartbeatService;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(HeartbeatAckEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		await _heartbeatService.Acknowledge(cancellationToken);
	}
}