using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Gateway.Contracts;
using Microsoft.Extensions.Hosting;

namespace Elixus.Discord.Hosted;

public sealed class HostedDiscordService : BackgroundService
{
	private readonly IDiscordApi _discordApi;
	private readonly IDiscordGateway _gateway;

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
