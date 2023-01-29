using Elixus.Discord.Core.Events.Gateway;
using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Handlers;
using Elixus.Discord.Gateway.Events.Serializers;
using Elixus.Discord.Gateway.Events.Serializers.Core;
using Elixus.Discord.Gateway.Hosted;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds Discord WS Gateway related services.
	/// </summary>
	public static void AddElixusDiscordGateway(this IServiceCollection services)
	{
		services.AddSingleton<HostedHeartbeatService>();
		services.AddSingleton<IHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());
		services.AddHostedService(provider => provider.GetRequiredService<HostedHeartbeatService>());

		services.AddSingleton<IDiscordGateway, DefaultDiscordGateway>();
		services.AddSingleton<IDispatchEventHandler, DispatchEventHandler>();
		services.AddSingleton<IEventHandler<HelloEvent>, HelloEventHandler>();
		services.AddSingleton<IEventSerializer<HelloEvent>, HelloEventSerializer>();
		services.AddSingleton<IEventHandler<HeartbeatEvent>, HeartbeatEventHandler>();
		services.AddSingleton<IEventSerializer<HeartbeatEvent>, HeartbeatEventSerializer>();
		services.AddSingleton<IEventHandler<HeartbeatAckEvent>, HeartbeatAckEventHandler>();
		services.AddSingleton<IEventSerializer<HeartbeatAckEvent>, HeartbeatAckEventSerializer>();
		services.AddSingleton<IEventHandler<InvalidSessionEvent>, InvalidSessionEventHandler>();
		services.AddSingleton<IEventSerializer<InvalidSessionEvent>, InvalidSessionEventSerializer>();
		services.AddSingleton<IEventHandler<ReconnectEvent>, ReconnectEventHandler>();
		services.AddSingleton<IEventSerializer<ReconnectEvent>, ReconnectEventSerializer>();
		services.AddSingleton<IEventSerializer<IdentifyEvent>, IdentifyEventSerializer>();
		services.AddSingleton<IEventSerializer<ResumeEvent>, ResumeEventSerializer>();

		// Core
		services.AddSingleton<IEventSerializer<ReadyEvent>, ReadyEventSerializer>();
	}
}
