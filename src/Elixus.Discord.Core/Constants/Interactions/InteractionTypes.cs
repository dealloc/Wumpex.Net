namespace Elixus.Discord.Core.Constants.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-type" />
public enum InteractionTypes
{
	/// <summary>
	/// PING
	/// </summary>
	Ping = 1,

	/// <summary>
	/// APPLICATION_COMMAND
	/// </summary>
	ApplicationCommand = 2,

	/// <summary>
	/// MESSAGE_COMPONENT
	/// </summary>
	MessageComponent = 3,

	/// <summary>
	/// APPLICATION_COMMAND_AUTOCOMPLETE
	/// </summary>
	ApplicationCommandAutocomplete = 4,

	/// <summary>
	/// MODAL_SUBMIT
	/// </summary>
	ModalSubmit = 5
}