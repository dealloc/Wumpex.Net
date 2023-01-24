using Elixus.Discord.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Core.Extensions;

/// <summary>
/// Contains extension methods for adding Elixus.Discord.Api to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	public static void AddElixusDiscordCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<DiscordConfiguration>(configuration);
		services.Configure<DiscordApiConfiguration>(configuration.GetSection("Api"));
	}
}
