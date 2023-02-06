using Elixus.Discord.Core.Attributes;
using Elixus.Discord.Core.Constants.Gateway;
using Elixus.Discord.Core.Models.Interactions;

namespace Elixus.Discord.Core.Events.Interactions;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#interaction-create" />
[Intent(GatewayIntents.Default)]
public sealed class InteractionCreateEvent : Interaction
{
	//
}