namespace Elixus.Discord.Core.Constants.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-type" />
public enum InteractionCallbackTypes
{
	/// <summary>
	/// ACK a Ping.
	/// </summary>
	Pong = 1,

	/// <summary>
	/// Respond to an interaction with a message.
	/// </summary>
	ChannelMessageWithSource = 4,

	/// <summary>
	/// ACK an interaction and edit a response later, the user sees a loading state.
	/// </summary>
	DeferredChannelMessageWithSource = 5,

	/// <summary>
	/// for components, ACK an interaction and edit the original message later;
	/// the user does not see a loading state.
	/// </summary>
	DeferredUpdateMessage = 6,

	/// <summary>
	/// For components, edit the message the component was attached to.
	/// </summary>
	UpdateMessage = 7,

	/// <summary>
	/// Respond to an autocomplete interaction with suggested choices.
	/// </summary>
	ApplicationCommandAutocompleteResult = 8,

	/// <summary>
	/// Respond to an interaction with a popup modal.
	/// </summary>
	Modal = 9
}