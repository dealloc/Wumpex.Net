using Elixus.Discord.Core.Events.Gateway;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.Logging;

namespace Elixus.Discord.Gateway.Events.Handlers.Core;

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