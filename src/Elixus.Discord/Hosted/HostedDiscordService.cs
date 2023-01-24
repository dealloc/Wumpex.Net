using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Gateway.Contracts;
using Microsoft.Extensions.Hosting;

namespace Elixus.Discord.Hosted;

/// <summary>
/// Runs the main Discord services from a .NET background service.
/// </summary>
public sealed class HostedDiscordService : BackgroundService
{
	private readonly IDiscordApi _discordApi;
	private readonly IDiscordGateway _gateway;

	/// <summary>
	/// Creates a new instance of <see cref="HostedDiscordService" />.
	/// </summary>
	public HostedDiscordService(IDiscordApi discordApi, IDiscordGateway gateway)
	{
		_discordApi = discordApi;
		_gateway = gateway;
	}

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var response = await _discordApi.GetGatewayBotAsync(stoppingToken);
		await _gateway.RunAsync(response.Url, stoppingToken);

		return;
	}
}
