using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Guilds;

namespace Wumpex.Net.Core.Events.Guilds;

/// <summary>
/// Sent when a guild becomes or was already unavailable due to an outage, or when the user leaves or is removed from a guild.
/// The inner payload is an unavailable guild object.
/// If the <see cref="UnavailableGuild.Unavailable" /> field is not set, the user was removed from the guild.
/// </summary>
[Intent(GatewayIntents.Guilds)]
public class GuildDeleteEvent : UnavailableGuild
{
	//
}