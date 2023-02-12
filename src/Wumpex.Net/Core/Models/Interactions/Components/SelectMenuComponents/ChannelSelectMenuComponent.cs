using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Channels;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions.Components.SelectMenuComponents;

/// <see href="https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-menu-structure" />
public class ChannelSelectMenuComponent : SelectMenuComponent
{
	/// <inheritdoc cref="Component.Type" />
	public override ComponentTypes Type { get; set; } = ComponentTypes.ChannelSelect;

	/// <summary>
	/// List of channel types to include in the channel select component.
	/// </summary>
	[JsonPropertyName("channel_types")]
	public List<ChannelTypes> ChannelTypes { get; set; } = new(0);
}