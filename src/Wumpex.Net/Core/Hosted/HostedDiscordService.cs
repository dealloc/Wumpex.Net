using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wumpex.Net.Api.Contracts;
using Wumpex.Net.Core.Contracts;
using Wumpex.Net.Core.Exceptions;
using Wumpex.Net.Core.Services;
using Wumpex.Net.Gateway.Contracts;

namespace Wumpex.Net.Core.Hosted;

/// <summary>
/// Runs the main Discord services from a .NET background service.
/// </summary>
public sealed class HostedDiscordService : BackgroundService
{
	private readonly ILogger<HostedDiscordService> _logger;
	private readonly IWumpex _wumpex;
	private readonly IDiscordApi _discordApi;
	private readonly IDiscordGateway _gateway;

	/// <summary>
	/// Creates a new instance of <see cref="HostedDiscordService" />.
	/// </summary>
	public HostedDiscordService(ILogger<HostedDiscordService> logger, IWumpex wumpex, IDiscordApi discordApi, IDiscordGateway gateway)
	{
		_logger = logger;
		_wumpex = wumpex;
		_discordApi = discordApi;
		_gateway = gateway;
	}

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		// linked allows us to manually cancel pending async.
		var linked = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

		_wumpex.User = _discordApi.GetCurrentUser(linked.Token);
		var gateway = await _discordApi.GetGatewayBotAsync(linked.Token);
		var endpoint = gateway.Url;

		while (stoppingToken.IsCancellationRequested is false)
		{
			try
			{
				await _gateway.RunAsync(endpoint!, linked.Token);
			}
			catch (DiscordException exception) when (exception.CanRecover is true)
			{
				if (await _gateway.CanRecover is false)
					throw;

				endpoint = await _gateway.ResumeEndpoint;
				_logger.LogWarning(exception, "Gateway caught an exception and is attempting to resume");

				linked.Cancel();
				linked = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
			}
		}

		_logger.LogInformation("Discord bot is shutting down");
	}
}
