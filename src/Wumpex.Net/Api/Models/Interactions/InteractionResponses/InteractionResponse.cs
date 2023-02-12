using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Api.Models.Interactions.InteractionResponses;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#responding-to-an-interaction" />
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ModalInteractionResponse), (int)InteractionCallbackTypes.Modal)]
[JsonDerivedType(typeof(MessageInteractionResponse), (int)InteractionCallbackTypes.ChannelMessageWithSource)]
public abstract class InteractionResponse
{
	/// <summary>
	/// The type of response.
	/// </summary>
	[JsonPropertyName("type")]
	public virtual InteractionCallbackTypes Type { get; }
}

/// <inheritdoc cref="InteractionResponse" />
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

/// <inheritdoc cref="InteractionResponse" />
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