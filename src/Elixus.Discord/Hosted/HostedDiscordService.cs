using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Core.Exceptions;
using Elixus.Discord.Gateway.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elixus.Discord.Hosted;

/// <summary>
/// Runs the main Discord services from a .NET background service.
/// </summary>
public sealed class HostedDiscordService : BackgroundService
{
	private readonly ILogger<HostedDiscordService> _logger;
	private readonly IDiscordApi _discordApi;
	private readonly IDiscordGateway _gateway;

	/// <summary>
	/// Creates a new instance of <see cref="HostedDiscordService" />.
	/// </summary>
	public HostedDiscordService(ILogger<HostedDiscordService> logger, IDiscordApi discordApi, IDiscordGateway gateway)
	{
		_logger = logger;
		_discordApi = discordApi;
		_gateway = gateway;
	}

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		// linked allows us to manually cancel pending async.
		var linked = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
		var response = await _discordApi.GetGatewayBotAsync(linked.Token);
		var endpoint = response.Url;

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
