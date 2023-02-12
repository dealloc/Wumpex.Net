using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Channels;

namespace Wumpex.Net.Core.Events.Channels;

/// <summary>
/// Sent when a channel relevant to the current user is deleted.
/// The inner payload is a channel object.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#channel-delete" />
[Intent(GatewayIntents.Guilds)]
[Intent(GatewayIntents.DirectMessages)]
public class ChannelDeleteEvent : Channel
{
	//
}