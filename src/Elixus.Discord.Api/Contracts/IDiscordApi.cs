using Elixus.Discord.Api.Exceptions;
using Elixus.Discord.Api.Models.Gateway;

namespace Elixus.Discord.Api.Contracts;

/// <summary>
/// Provides interaction with the Discord REST API.
/// </summary>
public interface IDiscordApi
{
	/// <summary>
	/// Get Gateway Bot.
	/// </summary>
	/// <exception cref="UnexpectedResponseException">Thrown if the gateway returns an unexpected response.</exception>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway#get-gateway-bot" />
	Task<GatewayBotResponse> GetGatewayBotAsync(CancellationToken cancellationToken = default);
}