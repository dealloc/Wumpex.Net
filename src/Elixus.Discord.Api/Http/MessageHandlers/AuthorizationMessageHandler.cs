using System.Net.Http.Headers;
using Elixus.Discord.Api.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elixus.Discord.Api.Http.MessageHandlers;

/// <summary>
/// A <see cref="DelegatingHandler" /> that adds the required authorization header to outgoing HTTP requests.
/// </summary>
internal sealed class AuthorizationMessageHandler : DelegatingHandler
{
	private readonly ILogger<AuthorizationMessageHandler> _logger;
	private readonly IOptionsMonitor<DiscordApiConfiguration> _monitor;

	public AuthorizationMessageHandler(ILogger<AuthorizationMessageHandler> logger, IOptionsMonitor<DiscordApiConfiguration> monitor)
	{
		_logger = logger;
		_monitor = monitor;
	}

	/// <inheritdoc cref="DelegatingHandler.Send" />
	protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		AddAuthorizationHeader(request);

		return base.Send(request, cancellationToken);
	}

	/// <inheritdoc cref="DelegatingHandler.SendAsync" />
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		AddAuthorizationHeader(request);

		return base.SendAsync(request, cancellationToken);
	}

	/// <summary>
	/// Adds the 'Authorization' header as required by Discord to authenticate the bot API calls.
	/// </summary>
	private void AddAuthorizationHeader(HttpRequestMessage request)
	{
		var token = _monitor.CurrentValue.Token;
		if (string.IsNullOrEmpty(token))
			_logger.LogWarning("The Discord API token is not correctly configured, API calls will fail if they require a token");

		request.Headers.Authorization = new AuthenticationHeaderValue("Bot", token);
	}
}
