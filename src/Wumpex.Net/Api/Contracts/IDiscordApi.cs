using Wumpex.Net.Api.Exceptions;
using Wumpex.Net.Api.Models.Channels;
using Wumpex.Net.Api.Models.Gateway;
using Wumpex.Net.Api.Models.Interactions.ApplicationCommands;
using Wumpex.Net.Api.Models.Interactions.InteractionResponses;
using Wumpex.Net.Api.Models.Webhooks;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Interactions;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Api.Contracts;

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
	/// Returns the user object of the requester's account.
	/// For OAuth2, this requires the identify scope, which will return the object without an email, and optionally the email scope, which returns the object with an email.
	/// </summary>
	/// <exception cref="UnexpectedResponseException">Thrown if the gateway returns an unexpected response.</exception>
	/// <seealso href="https://discord.com/developers/docs/resources/user#get-current-user" />
	Task<User> GetCurrentUser(CancellationToken cancellationToken = default);

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

	/// <summary>
	/// Create a followup message for an Interaction.
	/// Functions the same as Execute Webhook, but wait is always true.
	/// The thread_id, avatar_url, and username parameters are not supported when using this endpoint for interaction followups.
	/// </summary>
	/// <remarks>
	/// flags can be set to 64 to mark the message as ephemeral, except when it is the first followup message to a deferred Interactions Response.
	/// In that case, the flags field will be ignored, and the ephemerality of the message will be determined by the flags value in your original ACK.
	/// </remarks>
	/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response" />
	Task<Message> FollowUpInteraction(Interaction interaction, ExecuteWebhookRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Edits a followup message for an Interaction.
	/// Functions the same as Edit Webhook Message.
	/// </summary>
	/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#edit-original-interaction-response" />
	Task EditFollowUpInteraction(Interaction interaction, Message message, EditWebhookMessageRequest request, CancellationToken cancellationToken = default);
}