using System.Net.Http.Json;
using Elixus.Discord.Api.Contracts;
using Elixus.Discord.Api.Exceptions;
using Elixus.Discord.Api.Models.Gateway;

namespace Elixus.Discord.Api.Http;

/// <summary>
/// Implements the <see cref="IDiscordApi" /> contract using the <see cref="HttpClient" />.
/// </summary>
internal sealed class HttpDiscordApiClient : IDiscordApi
{
	private readonly HttpClient _http;
	private const string _endpoint = "https://discord.com/api/v10";

	public HttpDiscordApiClient(IHttpClientFactory httpClientFactory)
	{
		_http = httpClientFactory.CreateClient(Constants.HTTP_CLIENT_NAME);
	}

	/// <inheritdoc cref="IDiscordApi.GetGatewayBotAsync" />
	public async Task<GatewayBotResponse> GetGatewayBotAsync(CancellationToken cancellationToken = default)
	{
		var response = await _http.GetFromJsonAsync<GatewayBotResponse>($"{_endpoint}/gateway/bot", cancellationToken);

		return response ?? throw new UnexpectedResponseException("/gateway/bot");
	}
}
