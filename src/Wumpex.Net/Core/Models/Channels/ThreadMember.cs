using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Guilds;

namespace Wumpex.Net.Core.Models.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#thread-member-object" />
public sealed class ThreadMember
{
	/// <summary>
	/// ID of the thread.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// ID of the user.
	/// </summary>
	[JsonPropertyName("user_id")]
	public string? UserId { get; set; }

	/// <summary>
	/// Time the user last joined the thread.
	/// </summary>
	[JsonPropertyName("join_timestamp")]
	public DateTime JoinTimestamp { get; set; }

	/// <summary>
	/// Any user-thread settings, currently only used for notifications.
	/// </summary>
	[JsonPropertyName("flags")]
	public int Flags { get; set; }

	/// <summary>
	/// Additional information about the user.
	/// </summary>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }
}