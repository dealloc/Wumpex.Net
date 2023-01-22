using Elixus.Discord.Api.Contracts;
using Microsoft.Extensions.Hosting;

namespace Elixus.Discord.Hosted;

public sealed class HostedDiscordService : BackgroundService
{
	private readonly IDiscordApi _discordApi;

	public HostedDiscordService(IDiscordApi discordApi)
	{
		_discordApi = discordApi;
	}

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var response = await _discordApi.GetGatewayBotAsync(stoppingToken);

		return;
	}
}
