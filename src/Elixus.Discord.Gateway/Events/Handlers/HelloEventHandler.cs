using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;

namespace Elixus.Discord.Gateway.Events.Handlers;

/// <summary>
/// Handles the <see cref="HelloEvent" />.
/// This handler will start the <see cref="IHeartbeatService" />.
/// </summary>
/// <seealso cref="IHeartbeatService" />
internal sealed class HelloEventHandler : IEventHandler<HelloEvent>
{
	private readonly IHeartbeatService _heartbeatService;
	private readonly IDiscordGateway _discordGateway;

	public HelloEventHandler(IHeartbeatService heartbeatService, IDiscordGateway discordGateway)
	{
		_heartbeatService = heartbeatService;
		_discordGateway = discordGateway;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(HelloEvent @event, CancellationToken cancellationToken)
	{
		await _heartbeatService.Start(@event.HeartbeatInterval, cancellationToken);
	}
}
