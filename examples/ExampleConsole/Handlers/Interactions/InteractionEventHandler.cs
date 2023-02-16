using Wumpex.Net.Api.Contracts;
using Wumpex.Net.Api.Models.Interactions.InteractionResponses;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Models.Interactions;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;
using Microsoft.Extensions.Logging;
using Wumpex.Net.Api.Models.Webhooks;
using Wumpex.Net.Voice.Contracts;

namespace ExampleConsole.Handlers.Interactions;

/// <summary>
/// Example handler to an interaction.
/// </summary>
public class InteractionEventHandler : IEventHandler<InteractionCreateEvent>
{
	private readonly ILogger<InteractionEventHandler> _logger;
	private readonly IDiscordApi _discordApi;
	private readonly IVoiceService _voiceService;

	/// <summary>
	/// Creates a new instance of <see cref="InteractionEventHandler" />
	/// </summary>
	public InteractionEventHandler(ILogger<InteractionEventHandler> logger, IDiscordApi discordApi, IVoiceService voiceService)
	{
		_logger = logger;
		_discordApi = discordApi;
		_voiceService = voiceService;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(InteractionCreateEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Responding to {Id} - {Token}", @event.Interaction.Id, @event.Interaction.Token);

		if (@event.Interaction is ApplicationCommandInteraction interaction && string.Equals(interaction.Data?.Name, "connect"))
		{
			var channel = interaction.Data?.Resolved?.Channels?.Values?.FirstOrDefault();
			await _discordApi.RespondToInteraction(@event.Interaction, new DeferredMessageInteractionResponse(), cancellationToken);

			await _voiceService.ConnectAsync(interaction.GuildId!, channel?.Id!, cancellationToken);

			await _discordApi.FollowUpInteraction(@event.Interaction, new ExecuteWebhookRequest
			{
				Wait = true,
				Content = "Connected to voice!"
			}, cancellationToken);
		}
	}
}