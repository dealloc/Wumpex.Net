using Elixus.Discord.Api.Configurations;
using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Api.Http;
using Elixus.Discord.Api.Http.MessageHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elixus.Discord.Api.Extensions;

/// <summary>
/// Contains extension methods for adding Elixus.Discord.Api to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	public static void AddElixusDiscordApi(this IServiceCollection services, IConfiguration configuration)
	{
		var version = Assembly
			.GetExecutingAssembly()
			.GetName().Version
			?.ToString() ?? "1.0.0";

		services.Configure<DiscordApiConfiguration>(configuration);
		services.TryAddSingleton<IDiscordApi, HttpDiscordApiClient>();
		services.AddSingleton<AuthorizationMessageHandler>();
		services.AddHttpClient(Constants.HTTP_CLIENT_NAME, http =>
		{
			http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Elixus.Discord", version));
		}).AddHttpMessageHandler<AuthorizationMessageHandler>();
	}
}
