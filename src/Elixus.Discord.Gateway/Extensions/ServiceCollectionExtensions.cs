using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.EventHandlers;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Hosted;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddElixusDiscordGateway(this IServiceCollection services)
	{
		services.AddSingleton<HostedHeartbeatService>();
		services.AddSingleton<IHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());
		services.AddHostedService<HostedHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());

		services.AddSingleton<IDiscordGateway, DefaultDiscordGateway>();

		services.AddScoped<IEventHandler<HelloEvent>, HelloEventHandler>();
		services.AddScoped<IEventHandler<HeartbeatEvent>, HeartbeatEventHandler>();
		services.AddScoped<IEventHandler<HeartbeatAckEvent>, HeartbeatAckEventHandler>();
		services.AddScoped<IEventHandler<InvalidSessionEvent>, InvalidSessionEventHandler>();
		services.AddScoped<IEventHandler<ReconnectEvent>, ReconnectEventHandler>();
	}
}
