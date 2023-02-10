using System.Text.Json.Serialization;
using Elixus.Discord.Core.Attributes;
using Elixus.Discord.Core.Constants.Gateway;
using Elixus.Discord.Core.Models.Interactions;

namespace Elixus.Discord.Core.Events.Interactions;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#interaction-create" />
[Intent(GatewayIntents.Default)]
public class InteractionCreateEvent
{
	/// <summary>
	/// The interaction that was created in the event.
	/// </summary>
	[JsonPropertyName("interaction")]
	public Interaction Interaction { get; set; } = null!;
}
