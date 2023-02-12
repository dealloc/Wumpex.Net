using System.Text.Json.Serialization;
using Wumpex.Net.Core.Models.Interactions.Components.SelectMenuComponents;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-message-component-data-structure" />
public class MessageComponentData : InteractionData
{
	/// <summary>
	/// The custom_id of the component.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string CustomId { get; set; } = null!;

	/// <summary>
	/// The type of the component.
	/// </summary>
	[JsonPropertyName("component_type")]
	public ComponentTypes ComponentType { get; set; }

	/// <summary>
	/// Values the user selected in a select menu component.
	/// </summary>
	[JsonPropertyName("values")]
	public List<string>? Values { get; set; }
}