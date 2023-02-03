using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Guilds;
using Elixus.Discord.Core.Models.Users;

namespace Elixus.Discord.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#message-interaction-object-message-interaction-structure" />
public class MessageInteraction
{
	/// <summary>
	/// ID of the interaction.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// Type of interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public InteractionType Type { get; set; }

	/// <summary>
	/// Name of the application command, including subcommands and subcommand groups.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// User who invoked the interaction.
	/// </summary>
	[JsonPropertyName("user")]
	public User User { get; set; } = null!;

	/// <summary>
	/// Member who invoked the interaction in the guild.
	/// </summary>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }
}