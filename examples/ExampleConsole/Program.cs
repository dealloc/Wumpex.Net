using Wumpex.Net.Api.Extensions;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Extensions;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Extensions;
using ExampleConsole.Handlers.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wumpex.Net.Core.Hosted;

await Host.CreateDefaultBuilder()
	.ConfigureServices((host, services) =>
	{
		services.AddWumpexCore(host.Configuration.GetSection("Wumpex.Net"));
		services.AddWumpexApi();
		services.AddWumpexGateway();
		services.AddDiscordIntents(GatewayIntents.Guilds, GatewayIntents.GuildMessages, GatewayIntents.DirectMessages);
		services.AddHostedService<HostedDiscordService>();
		services.AddScoped<IEventHandler<InteractionCreateEvent>, InteractionEventHandler>();
	})
	.Build()
	.RunAsync();