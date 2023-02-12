using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Interactions;

namespace Wumpex.Net.Core.Events.Interactions;

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
