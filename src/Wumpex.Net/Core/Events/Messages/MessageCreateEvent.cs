using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Guilds;

namespace Wumpex.Net.Core.Events.Messages;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#message-create" />
[Intent(GatewayIntents.GuildMessages)]
[Intent(GatewayIntents.DirectMessages)]
public class MessageCreateEvent : Message
{
	/// <summary>
	/// ID of the guild the message was sent in - unless it is an ephemeral message.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// Member properties for this message's author.
	/// Missing for ephemeral messages and messages from webhooks.
	/// </summary>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }
}