using Microsoft.Extensions.Hosting;

namespace Elixus.Discord.Hosted;

public sealed class HostedDiscordService : BackgroundService
{
	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		//
	}
}
