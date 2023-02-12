using System.Text.Json.Serialization;
using Wumpex.Net.Api.Constants.Channels;
using Wumpex.Net.Core.Models.Permissions;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Api.Models.Channels;

/// <summary>
/// The allowed mention field allows for more granular control over mentions without various hacks to the message content.
/// This will always validate against message content to avoid phantom pings (e.g. to ping everyone, you must still have @everyone in the message content), and check against user/bot permissions.
/// </summary>
public sealed class AllowedMention
{
	/// <summary>
	/// An array of allowed mention types to parse from the content.
	/// </summary>
	[JsonPropertyName("type")]
	public List<AllowedMentionTypes> Parse { get; set; } = new(0);

	/// <summary>
	/// Array of <see cref="Role.Id" /> to mention (Max size of 100)
	/// </summary>
	[JsonPropertyName("roles")]
	public List<string> Roles { get; set; } = new(0);

	/// <summary>
	/// Array of <see cref="User.Id" /> to mention (Max size of 100).
	/// </summary>
	[JsonPropertyName("users")]
	public List<string> Users { get; set; } = new(0);

	/// <summary>
	/// For replies, whether to mention the author of the message being replied to (default false).
	/// </summary>
	[JsonPropertyName("replied_user")]
	public bool RepliedUser { get; set; } = false;
}