using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Channels;

namespace Wumpex.Net.Core.Models.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#message-object-message-activity-structure" />
public class MessageActivity
{
	/// <summary>
	/// Type of message activity.
	/// </summary>
	[JsonPropertyName("type")]
	public MessageActivityTypes Type { get; set; }

	/// <summary>
	/// party_id from a Rich Presence event.
	/// </summary>
	[JsonPropertyName("party_id")]
	public string? PartyId { get; set; }
}