using Wumpex.Net.Api.Contracts;
using Wumpex.Net.Api.Models.Interactions.InteractionResponses;
using Wumpex.Net.Core.Constants.Interactions;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Models.Interactions;
using Wumpex.Net.Core.Models.Interactions.Components;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;
using Microsoft.Extensions.Logging;

namespace ExampleConsole.Handlers.Interactions;

/// <summary>
/// Example handler to an interaction.
/// </summary>
public class InteractionEventHandler : IEventHandler<InteractionCreateEvent>
{
	private readonly ILogger<InteractionEventHandler> _logger;
	private readonly IDiscordApi _discordApi;

	/// <summary>
	/// Creates a new instance of <see cref="InteractionEventHandler" />
	/// </summary>
	public InteractionEventHandler(ILogger<InteractionEventHandler> logger, IDiscordApi discordApi)
	{
		_logger = logger;
		_discordApi = discordApi;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public async ValueTask HandleEvent(InteractionCreateEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Responding to {Id} - {Token}", @event.Interaction.Id, @event.Interaction.Token);

		if (@event.Interaction.Type is not InteractionTypes.ModalSubmit)
			await RespondWithModal(@event.Interaction, cancellationToken);
		else if (@event.Interaction is ModalSubmitInteraction submit)
		{
			_logger.LogInformation("Responding to submit with message!");
			await _discordApi.RespondToInteraction(@event.Interaction, new MessageInteractionResponse
			{
				Data = new()
				{
					Content = $"We received {submit.Data?.Components?.FirstOrDefault()?.Components?.FirstOrDefault()?.Value}"
				}
			}, cancellationToken);
		}
	}

	private Task RespondWithModal(Interaction interaction, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Responding with modal");

		return _discordApi.RespondToInteraction(interaction, new ModalInteractionResponse
		{
			Data = new InteractionCallbackModal
			{
				CustomId = "modal_id",
				Title = "This is a modal",
				Components = new()
				{
					new ActionRowComponent()
					{
						Components = new()
						{
							new TextInputComponent
							{
								CustomId = "name",
								Label = "Name",
								Style = TextInputStyles.Short,
								MinLength = 1,
								MaxLength = 4000,
								Required = true
							}
						}
					}
				}
			}
		}, cancellationToken);
	}
}