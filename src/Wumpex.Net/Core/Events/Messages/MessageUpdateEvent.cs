using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Channels;

namespace Wumpex.Net.Core.Events.Messages;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#message-update" />
[Intent(GatewayIntents.GuildMessages)]
[Intent(GatewayIntents.DirectMessages)]
public class MessageUpdateEvent : Message
{
	//
}