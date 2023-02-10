using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Interactions.Components.SelectMenuComponents;

namespace Elixus.Discord.Core.Models.Interactions;

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