using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Reflection;
using Wumpex.Net.Api.Contracts;
using Wumpex.Net.Api.Http;
using Wumpex.Net.Api.Http.MessageHandlers;

namespace Wumpex.Net.Api.Extensions;

/// <summary>
/// Contains extension methods for adding Wumpex.Net.Api to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds Discord REST API related services to the container.
	/// </summary>
	public static void AddWumpexApi(this IServiceCollection services)
	{
		var version = Assembly
			.GetExecutingAssembly()
			.GetName().Version
			?.ToString() ?? "1.0.0";

		services.TryAddSingleton<IDiscordApi, HttpDiscordApiClient>();
		services.AddSingleton<AuthorizationMessageHandler>();
		services.AddHttpClient("wumpex.net.api", http =>
		{
			http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Wumpex.Net", version));
		}).AddHttpMessageHandler<AuthorizationMessageHandler>();
	}
}
