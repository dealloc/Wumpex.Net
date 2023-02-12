namespace Wumpex.Net.Core.Constants.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/message-components" />
public enum ComponentTypes
{
	/// <summary>
	/// Container for other components.
	/// </summary>
	ActionRow = 1,

	/// <summary>
	/// Button object.
	/// </summary>
	Button = 2,

	/// <summary>
	/// Select menu for picking from defined text options.
	/// </summary>
	StringSelect = 3,

	/// <summary>
	/// Text input object.
	/// </summary>
	TextInput = 4,

	/// <summary>
	/// Select menu for users.
	/// </summary>
	UserSelect = 5,

	/// <summary>
	/// Select menu for roles.
	/// </summary>
	RoleSelect = 6,

	/// <summary>
	/// Select menu for mentionables (users and roles).
	/// </summary>
	MentionableSelect = 7,

	/// <summary>
	/// Select menu for channels.
	/// </summary>
	ChannelSelect = 8
}