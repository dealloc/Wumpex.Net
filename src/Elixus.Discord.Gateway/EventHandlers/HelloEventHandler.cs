using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Events;

namespace Elixus.Discord.Gateway.EventHandlers;

/// <summary>
/// Handles the <see cref="HelloEvent" />.
/// This handler will start the <see cref="IHeartbeatService" />.
/// </summary>
/// <seealso cref="IHeartbeatService" />
internal sealed class HelloEventHandler : IEventHandler<HelloEvent>
{
	private readonly IHeartbeatService _heartbeatService;

	public HelloEventHandler(IHeartbeatService heartbeatService)
	{
		_heartbeatService = heartbeatService;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(HelloEvent @event, CancellationToken cancellationToken)
	{
		await _heartbeatService.Start(@event.HeartbeatInterval, cancellationToken);
	}
}
