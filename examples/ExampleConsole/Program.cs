using Elixus.Discord.Api.Extensions;
using Elixus.Discord.Gateway.Extensions;
using Elixus.Discord.Hosted;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
	.ConfigureServices((host, services) =>
	{
		services.AddElixusDiscordApi(host.Configuration.GetSection("Elixus.Discord:Api"));
		services.AddElixusDiscordGateway();
		services.AddHostedService<HostedDiscordService>();
	})
	.Build()
	.RunAsync();