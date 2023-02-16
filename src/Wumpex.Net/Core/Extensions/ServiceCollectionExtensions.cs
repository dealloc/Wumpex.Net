using Wumpex.Net.Core.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wumpex.Net.Core.Configurations;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Contracts;
using Wumpex.Net.Core.Services;

namespace Wumpex.Net.Core.Extensions;

/// <summary>
/// Contains extension methods for adding Wumpex.Net.Api to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds core services and configuration for Wumpex.Net libraries.
	/// </summary>
	public static void AddWumpexCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<DiscordConfiguration>(configuration);
		services.AddSingleton<IWumpex, DefaultWumpex>();
	}

	/// <summary>
	/// Configures the gateway to request all given intents.
	/// </summary>
	public static void AddDiscordIntents(this IServiceCollection services, params GatewayIntents[] intents)
	{
		var requested = intents
			.Aggregate(GatewayIntents.Default, (total, intent) => total | intent);

		services.Configure<DiscordConfiguration>(config =>
		{
			config.Gateway.Intents = requested;
		});
	}
}
