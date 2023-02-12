using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;

namespace Wumpex.Net.Core.Events.Messages;

/// <summary>
/// Sent when a message is deleted.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#message-delete" />
[Intent(GatewayIntents.GuildMessages)]
[Intent(GatewayIntents.DirectMessages)]
public sealed class MessageDeleteEvent
{
	/// <summary>
	/// ID of the message.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// ID of the channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; } = null!;

	/// <summary>
	/// ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }
}