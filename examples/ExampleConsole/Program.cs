using Elixus.Discord.Hosted;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
	.ConfigureServices((host, services) =>
	{
		services.AddHostedService<HostedDiscordService>();
	})
	.Build()
	.RunAsync();