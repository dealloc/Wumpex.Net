namespace Elixus.Discord.Core.Constants.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-type" />
public enum ApplicationCommandOptionTypes
{
	/// <summary>
	/// SUB_COMMAND
	/// </summary>
	SubCommand = 1,

	/// <summary>
	/// SUB_COMMAND_GROUP
	/// </summary>
	SubCommandGroup = 2,

	/// <summary>
	/// STRING
	/// </summary>
	String = 3,

	/// <summary>
	/// INTEGER
	/// </summary>
	/// <remarks>
	/// Any integer between -2^53 and 2^53.
	/// </remarks>
	Integer = 4,

	/// <summary>
	/// BOOLEAN
	/// </summary>
	Boolean = 5,

	/// <summary>
	/// USER
	/// </summary>
	User = 6,

	/// <summary>
	/// CHANNEL
	/// </summary>
	/// <remarks>
	/// Includes all channel types + categories.
	/// </remarks>
	Channel = 7,

	/// <summary>
	/// ROLE
	/// </summary>
	Role = 8,

	/// <summary>
	/// MENTIONABLE
	/// </summary>
	/// <remarks>
	/// Includes users and roles.
	/// </remarks>
	Mentionable = 9,

	/// <summary>
	/// NUMBER
	/// </summary>
	/// <remarks>
	/// Any double between -2^53 and 2^53.
	/// </remarks>
	Number = 10,

	/// <summary>
	/// ATTACHMENT
	/// </summary>
	/// <remarks>
	/// Attachment object.
	/// </remarks>
	Attachment = 11
}