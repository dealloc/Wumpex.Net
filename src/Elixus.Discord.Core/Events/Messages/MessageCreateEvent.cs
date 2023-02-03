using System.Text.Json.Serialization;
using Elixus.Discord.Core.Attributes;
using Elixus.Discord.Core.Constants.Gateway;
using Elixus.Discord.Core.Models.Channels;
using Elixus.Discord.Core.Models.Guilds;
using Elixus.Discord.Core.Models.Users;

namespace Elixus.Discord.Core.Events.Messages;

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