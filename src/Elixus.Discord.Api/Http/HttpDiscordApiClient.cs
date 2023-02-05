using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Api.Exceptions;
using Elixus.Discord.Api.Models.Gateway;
using Elixus.Discord.Api.Models.Interactions.ApplicationCommands;
using Elixus.Discord.Api.Serialization;
using Elixus.Discord.Core.Configurations;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elixus.Discord.Api.Http;

/// <summary>
/// Implements the <see cref="IDiscordApi" /> contract using the <see cref="HttpClient" />.
/// </summary>
internal sealed class HttpDiscordApiClient : IDiscordApi
{
	private readonly HttpClient _http;
	private readonly ILogger<HttpDiscordApiClient> _logger;
	private readonly IOptionsMonitor<DiscordConfiguration> _monitor;

	private const string _endpoint = "https://discord.com/api/v10";

	public HttpDiscordApiClient(ILogger<HttpDiscordApiClient> logger, IHttpClientFactory httpClientFactory, IOptionsMonitor<DiscordConfiguration> monitor)
	{
		_logger = logger;
		_monitor = monitor;
		_http = httpClientFactory.CreateClient(Constants.HTTP_CLIENT_NAME);
	}

	/// <inheritdoc cref="IDiscordApi.GetGatewayBotAsync" />
	public async Task<GatewayBotResponse> GetGatewayBotAsync(CancellationToken cancellationToken = default)
	{
		var response = await _http.GetFromJsonAsync<GatewayBotResponse>($"{_endpoint}/gateway/bot", cancellationToken);

		return response ?? throw new UnexpectedResponseException("/gateway/bot");
	}

	/// <inheritdoc cref="IDiscordApi.CreateGlobalApplicationCommand" />
	public async Task<ApplicationCommand> CreateGlobalApplicationCommand(CreateApplicationCommandRequest request, CancellationToken cancellationToken = default)
	{
		var response = await _http.PostAsJsonAsync($"{_endpoint}/applications/{_monitor.CurrentValue.ApplicationId}/commands", request, cancellationToken);

		try
		{
			response.EnsureSuccessStatusCode();
			return (await response.Content.ReadFromJsonAsync<ApplicationCommand>(ApiSerializerContext.Default.ApplicationCommand, cancellationToken))!;
		}
		catch (HttpRequestException exception) when (response.Content.Headers.ContentLength > 0)
		{
			var body = await response.Content.ReadAsStringAsync(cancellationToken);

			_logger.LogError(exception, "Failed to create global application: {Message}", body);
			throw;
		}
	}

	/// <inheritdoc cref="IDiscordApi.GetGlobalApplicationCommands" />
	public async IAsyncEnumerable<ApplicationCommand> GetGlobalApplicationCommands(bool withLocalizations = false, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var stream = await _http.GetStreamAsync($"{_endpoint}/applications/{_monitor.CurrentValue.ApplicationId}/commands", cancellationToken);

		await foreach (var command in JsonSerializer.DeserializeAsyncEnumerable(stream, ApiSerializerContext.Default.ApplicationCommand, cancellationToken))
		{
			yield return command!;
		}
	}

	/// <inheritdoc cref="IDiscordApi.DeleteGlobalApplicationCommand" />
	public async Task DeleteGlobalApplicationCommand(string id, CancellationToken cancellationToken = default)
	{
		var response = await _http.DeleteAsync($"{_endpoint}/applications/{_monitor.CurrentValue.ApplicationId}/commands/{id}", cancellationToken);

		response.EnsureSuccessStatusCode();
	}
}
