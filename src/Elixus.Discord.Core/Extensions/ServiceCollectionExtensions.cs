using Elixus.Discord.Core.Configurations;
using Elixus.Discord.Core.Constants;
using Elixus.Discord.Core.Constants.Gateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Core.Extensions;

/// <summary>
/// Contains extension methods for adding Elixus.Discord.Api to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds core services and configuration for Elixus.Discord libraries.
	/// </summary>
	public static void AddElixusDiscordCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<DiscordConfiguration>(configuration);
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
