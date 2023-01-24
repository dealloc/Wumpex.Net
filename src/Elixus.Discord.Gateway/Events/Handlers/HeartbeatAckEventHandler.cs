using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Events.Handlers;

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
	public async ValueTask HandleEvent(HeartbeatAckEvent @event, CancellationToken cancellationToken)
	{
		await _heartbeatService.Acknowledge(cancellationToken);
	}
}