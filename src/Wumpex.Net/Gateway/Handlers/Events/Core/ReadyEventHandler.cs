using Microsoft.Extensions.Logging;
using Wumpex.Net.Core.Events.Gateway;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events.Core;

internal sealed class ReadyEventHandler : IEventHandler<ReadyEvent>
{
	private readonly ILogger<ReadyEventHandler> _logger;
	private readonly IDiscordGateway _discordGateway;

	public ReadyEventHandler(ILogger<ReadyEventHandler> logger, IDiscordGateway discordGateway)
	{
		_logger = logger;
		_discordGateway = discordGateway;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(ReadyEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		var endpoint = new Uri(@event.ResumeGatewayUrl);
		_logger.LogDebug("Configuring gateway to resume to {Endpoint}", endpoint);

		await _discordGateway.ConfigureReconnect(endpoint, @event.SessionId, cancellationToken);
	}
}