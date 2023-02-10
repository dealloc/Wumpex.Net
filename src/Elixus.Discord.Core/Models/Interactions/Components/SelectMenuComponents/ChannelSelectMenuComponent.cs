using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Channels;
using Elixus.Discord.Core.Constants.Interactions;

namespace Elixus.Discord.Core.Models.Interactions.Components.SelectMenuComponents;

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