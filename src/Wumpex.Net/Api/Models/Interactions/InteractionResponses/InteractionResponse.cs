using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

namespace Wumpex.Net.Api.Models.Interactions.InteractionResponses;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#responding-to-an-interaction" />
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PongInteractionResponse), (int)InteractionCallbackTypes.Pong)]
[JsonDerivedType(typeof(MessageInteractionResponse), (int)InteractionCallbackTypes.ChannelMessageWithSource)]
[JsonDerivedType(typeof(DeferredMessageInteractionResponse), (int)InteractionCallbackTypes.DeferredChannelMessageWithSource)]
[JsonDerivedType(typeof(DeferredUpdateMessageInteractionResponse), (int)InteractionCallbackTypes.DeferredUpdateMessage)]
[JsonDerivedType(typeof(UpdateMessageInteractionResponse), (int)InteractionCallbackTypes.UpdateMessage)]
[JsonDerivedType(typeof(ApplicationCommandAutoCompleteResultInteractionResponse), (int)InteractionCallbackTypes.ApplicationCommandAutocompleteResult)]
[JsonDerivedType(typeof(ModalInteractionResponse), (int)InteractionCallbackTypes.Modal)]
public abstract class InteractionResponse
{
	/// <summary>
	/// The type of response.
	/// </summary>
	[JsonPropertyName("type")]
	public virtual InteractionCallbackTypes Type { get; }
}

/// <inheritdoc cref="InteractionCallbackTypes.Pong" />
public class PongInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.Pong;
}

/// <inheritdoc cref="InteractionCallbackTypes.ChannelMessageWithSource" />
public class MessageInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.ChannelMessageWithSource;

	/// <summary>
	/// an optional response message.
	/// </summary>
	[JsonPropertyName("data")]
	public InteractionCallbackMessage Data { get; set; } = null!;
}

/// <inheritdoc cref="InteractionCallbackTypes.DeferredChannelMessageWithSource" />
public class DeferredMessageInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.DeferredChannelMessageWithSource;
}

/// <inheritdoc cref="InteractionCallbackTypes.DeferredUpdateMessage" />
public class DeferredUpdateMessageInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.DeferredUpdateMessage;
}

/// <inheritdoc cref="InteractionCallbackTypes.UpdateMessage" />
public class UpdateMessageInteractionResponse : InteractionResponse 
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.UpdateMessage;
}

/// <inheritdoc cref="InteractionCallbackTypes.ApplicationCommandAutocompleteResult" />
public class ApplicationCommandAutoCompleteResultInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.ApplicationCommandAutocompleteResult;

	/// <summary>
	/// Autocomplete choices (max of 25 choices).
	/// </summary>
	[JsonPropertyName("choices")]
	public List<ApplicationCommandOptionWithStringChoices> Choices { get; set; } = new(0);
}

/// <inheritdoc cref="InteractionCallbackTypes.Modal" />
public class ModalInteractionResponse : InteractionResponse
{
	/// <inheritdoc cref="InteractionResponse.Type" />
	public override InteractionCallbackTypes Type => InteractionCallbackTypes.Modal;

	/// <summary>
	/// an optional response message.
	/// </summary>
	[JsonPropertyName("data")]
	public InteractionCallbackModal? Data { get; set; } = null!;
}