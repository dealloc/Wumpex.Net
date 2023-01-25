using Elixus.Discord.Core.Configurations;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.Options;

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
	private readonly IOptionsMonitor<DiscordConfiguration> _monitor;

	public HelloEventHandler(IHeartbeatService heartbeatService, IDiscordGateway discordGateway, IOptionsMonitor<DiscordConfiguration> monitor)
	{
		_heartbeatService = heartbeatService;
		_discordGateway = discordGateway;
		_monitor = monitor;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(HelloEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		await _heartbeatService.Start(@event.HeartbeatInterval, cancellationToken);

		await _discordGateway.SendAsync(new IdentifyEvent
		{
			Token = _monitor.CurrentValue.Token ?? string.Empty,
			Properties = new()
			{
				OperatingSystem = "linux",
				Browser = "Elixus.Discord",
				Device = "Elixus.Discord"
			},
			Intents = 3145728,
		}, cancellationToken);
	}
}
