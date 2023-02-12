using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events;

/// <summary>
/// Handles the <see cref="HeartbeatEvent" />.
/// This will ask the <see cref="IHeartbeatService" /> to immediately send a heartbeat without waiting.
/// </summary>
internal sealed class HeartbeatEventHandler : IEventHandler<HeartbeatEvent>
{
	private readonly IHeartbeatService _heartbeat;

	public HeartbeatEventHandler(IHeartbeatService heartbeatService)
	{
		_heartbeat = heartbeatService;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(HeartbeatEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		await _heartbeat.RequestSend(cancellationToken);
	}
}