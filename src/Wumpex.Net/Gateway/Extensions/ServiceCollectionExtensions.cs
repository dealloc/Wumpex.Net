using Microsoft.Extensions.DependencyInjection;
using Wumpex.Net.Core.Events.Gateway;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Events.Voice;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Dispatch;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Handlers.Events;
using Wumpex.Net.Gateway.Handlers.Events.Core;
using Wumpex.Net.Gateway.Hosted;
using Wumpex.Net.Gateway.Serialization;
using Wumpex.Net.Gateway.Serialization.Core;
using Wumpex.Net.Gateway.Services;

namespace Wumpex.Net.Gateway.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds Discord WS Gateway related services.
	/// </summary>
	public static void AddWumpexGateway(this IServiceCollection services)
	{
		services.AddSingleton<HostedHeartbeatService>();
		services.AddSingleton<IHeartbeatService>(provider => provider.GetRequiredService<HostedHeartbeatService>());
		services.AddHostedService(provider => provider.GetRequiredService<HostedHeartbeatService>());

		services.AddSingleton<HostedWorkerPool>();
		services.AddHostedService(provider => provider.GetRequiredService<HostedWorkerPool>());

		services.AddSingleton(typeof(IEventSerializer<>), typeof(CoreEventSerializer<>));
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
		services.AddSingleton<IEventSerializer<VoiceStateUpdateEvent>, VoiceStateUpdateEventSerializer>();

		// Core
		services.AddSingleton<IEventHandler<ReadyEvent>, ReadyEventHandler>();
		services.AddSingleton<IEventSerializer<InteractionCreateEvent>, InteractionCreateEventSerializer>();
	}

	public static void AddDeferredEventListener<TEvent>(this IServiceCollection services) where TEvent : class
	{
		services.AddSingleton<IEventHandler<TEvent>>(provider => provider.GetRequiredService<IDeferredEventListener<TEvent>>());
		services.AddSingleton<IDeferredEventListener<TEvent>, DefaultDeferredEventListener<TEvent>>();
	}
}
