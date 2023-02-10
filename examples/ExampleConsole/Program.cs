using Elixus.Discord.Api.Extensions;
using Elixus.Discord.Core.Constants.Gateway;
using Elixus.Discord.Core.Events.Interactions;
using Elixus.Discord.Core.Extensions;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Extensions;
using Elixus.Discord.Hosted;
using ExampleConsole.Handlers.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
	.ConfigureServices((host, services) =>
	{
		services.AddElixusDiscordCore(host.Configuration.GetSection("Elixus.Discord"));
		services.AddElixusDiscordApi();
		services.AddElixusDiscordGateway();
		services.AddDiscordIntents(GatewayIntents.Guilds, GatewayIntents.GuildMessages);
		services.AddHostedService<HostedDiscordService>();
		services.AddScoped<IEventHandler<InteractionCreateEvent>, InteractionEventHandler>();
	})
	.Build()
	.RunAsync();