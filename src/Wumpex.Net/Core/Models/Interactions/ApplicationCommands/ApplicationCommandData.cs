using System.Diagnostics;
using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions.ApplicationCommands;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data" />
[DebuggerDisplay("{Name}")]
public sealed class ApplicationCommandData : InteractionData
{
	/// <summary>
	/// the ID of the invoked command.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The name of the invoked command.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// The type of the invoked command.
	/// </summary>
	[JsonPropertyName("type")]
	public ApplicationCommandTypes Type { get; set; }

	/// <summary>
	/// Converted users + roles + channels + attachments.
	/// </summary>
	[JsonPropertyName("resolved")]
	public ResolvedData? Resolved { get; set; }

	/// <summary>
	/// The params + values from the user.
	/// </summary>
	/// <remarks>
	/// This can be partial when in response to APPLICATION_COMMAND_AUTOCOMPLETE.
	/// </remarks>
	/// <seealso href="https://discord.com/developers/docs/interactions/application-commands#autocomplete" />
	[JsonPropertyName("options")]
	public List<ApplicationCommandInteractionDataOption>? Options { get; set; }

	/// <summary>
	/// The id of the guild the command is registered to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// Id of the user or message targeted by a user or message command.
	/// </summary>
	[JsonPropertyName("target_id")]
	public string? TargetId { get; set; }
}