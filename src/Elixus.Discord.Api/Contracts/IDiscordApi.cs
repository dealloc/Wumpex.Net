using Elixus.Discord.Api.Exceptions;
using Elixus.Discord.Api.Models.Channels;
using Elixus.Discord.Api.Models.Gateway;
using Elixus.Discord.Api.Models.Interactions.ApplicationCommands;
using Elixus.Discord.Api.Models.Interactions.InteractionResponses;
using Elixus.Discord.Core.Models.Channels;
using Elixus.Discord.Core.Models.Interactions;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

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

	/// <summary>
	/// Create a new global command. Returns 201 if a command with the same name does not already exist, or a 200 if it does (in which case the previous command will be overwritten).
	/// Both responses include an application command object.
	/// </summary>
	/// <returns>A <see cref="ApplicationCommand" /> object of the created/updated command.</returns>
	/// <seealso href="https://discord.com/developers/docs/interactions/application-commands#endpoints" />
	Task<ApplicationCommand> CreateGlobalApplicationCommand(CreateApplicationCommandRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Fetch all of the global commands for your application.
	/// Returns an array of application command objects.
	/// </summary>
	/// <remarks>
	/// The objects returned by this endpoint may be augmented with additional fields if localization is active.
	/// </remarks>
	IAsyncEnumerable<ApplicationCommand> GetGlobalApplicationCommands(bool withLocalizations = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a global command.
	/// Returns 204 No Content on success.
	/// </summary>
	Task DeleteGlobalApplicationCommand(string id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Post a message to a guild text or DM channel.
	/// Returns a message object.
	/// Fires a Message Create Gateway event.
	/// See message formatting for more information on how to properly format messages.
	/// </summary>
	/// <param name="channel">The snowflake ID of a channel to create the message in</param>
	/// <param name="request">The <see cref="CreateMessageRequest" /> detailing what message to create.</param>
	/// <param name="cancellationToken">Allows cancelling the request.</param>
	/// <seealso href="https://discord.com/developers/docs/resources/channel#create-message" />
	Task<Message> CreateMessage(string channel, CreateMessageRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Interactions--both receiving and responding--are webhooks under the hood.
	/// So responding to an Interaction is just like sending a webhook request!
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/interactions/receiving-and-responding#responding-to-an-interaction" />
	Task RespondToInteraction(Interaction interaction, InteractionResponse response, CancellationToken cancellationToken = default);
}