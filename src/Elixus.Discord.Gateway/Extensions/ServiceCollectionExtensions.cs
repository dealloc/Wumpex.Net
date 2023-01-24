using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Handlers;
using Elixus.Discord.Gateway.Events.Serializers;
using Elixus.Discord.Gateway.Hosted;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
	public static void AddElixusDiscordGateway(this IServiceCollection services)
	{
		services.AddSingleton<HostedHeartbeatService>();
		services.AddSingleton<IHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());
		services.AddHostedService<HostedHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());

		services.AddSingleton<IDiscordGateway, DefaultDiscordGateway>();

		services.AddScoped<IEventHandler<HelloEvent>, HelloEventHandler>();
		services.AddSingleton<IEventSerializer<HelloEvent>, HelloEventSerializer>();

		services.AddScoped<IEventHandler<HeartbeatEvent>, HeartbeatEventHandler>();
		services.AddSingleton<IEventSerializer<HeartbeatEvent>, HeartbeatEventSerializer>();

		services.AddScoped<IEventHandler<HeartbeatAckEvent>, HeartbeatAckEventHandler>();
		services.AddSingleton<IEventSerializer<HeartbeatAckEvent>, HeartbeatAckEventSerializer>();

		services.AddScoped<IEventHandler<InvalidSessionEvent>, InvalidSessionEventHandler>();
		services.AddSingleton<IEventSerializer<InvalidSessionEvent>, InvalidSessionEventSerializer>();

		services.AddScoped<IEventHandler<ReconnectEvent>, ReconnectEventHandler>();
		services.AddSingleton<IEventSerializer<ReconnectEvent>, ReconnectEventSerializer>();
	}
}
