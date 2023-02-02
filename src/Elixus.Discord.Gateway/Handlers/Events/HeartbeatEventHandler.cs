using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Handlers.Events;

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