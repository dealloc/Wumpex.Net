using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Guilds;
using Wumpex.Net.Core.Models.Permissions;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-resolved-data-structure" />
public sealed class ResolvedData
{
	/// <summary>
	/// The ids and User objects.
	/// </summary>
	[JsonPropertyName("users")]
	public Dictionary<string, User>? Users { get; set; }

	/// <summary>
	/// The ids and partial Member objects.
	/// </summary>
	/// <remarks>
	/// objects are missing user, deaf and mute fields
	/// </remarks>
	[JsonPropertyName("members")]
	public Dictionary<string, GuildMember>? Members { get; set; }

	/// <summary>
	/// The ids and Role objects.
	/// </summary>
	[JsonPropertyName("roles")]
	public Dictionary<string, Role>? Roles { get; set; }

	/// <summary>
	/// The ids and partial Channel objects.
	/// </summary>
	/// <remarks>
	/// Partial Channel objects only have id, name, type and permissions fields.
	/// Threads will also have thread_metadata and parent_id fields.
	/// </remarks>
	[JsonPropertyName("channels")]
	public Dictionary<string, Channel>? Channels { get; set; }

	/// <summary>
	/// The ids and partial Message objects.
	/// </summary>
	[JsonPropertyName("messages")]
	public Dictionary<string, Message>? Messages { get; set; }

	/// <summary>
	/// The ids and attachment objects.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Dictionary<string, Attachment>? Attachments { get; set; }
}