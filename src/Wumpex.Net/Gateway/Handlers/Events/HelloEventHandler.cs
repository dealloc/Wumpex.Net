using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using Wumpex.Net.Core.Configurations;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events;

/// <summary>
/// Handles the <see cref="HelloEvent" />.
/// This handler will start the <see cref="IHeartbeatService" />.
/// If the gateway is able to resume we'll send a RESUME, otherwise we send an IDENTIFY.
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

		if (await _discordGateway.CanRecover)
		{
			var session = await _discordGateway.ResumeSession;
			var sequence = await _heartbeatService.Sequence;

			await _discordGateway.SendAsync(new ResumeEvent
			{
				Token = _monitor.CurrentValue.Token ?? string.Empty,
				SessionId = session ?? string.Empty,
				Sequence = sequence ?? -1,
			}, cancellationToken);
		}
		else
		{
			await _discordGateway.SendAsync(new IdentifyEvent
			{
				Token = _monitor.CurrentValue.Token ?? string.Empty,
				Properties = new()
				{
					OperatingSystem = RuntimeInformation.OSDescription,
					Browser = "Wumpex.Net",
					Device = "Wumpex.Net"
				},
				Intents = (int)_monitor.CurrentValue.Gateway.Intents,
			}, cancellationToken);
		}
	}
}
