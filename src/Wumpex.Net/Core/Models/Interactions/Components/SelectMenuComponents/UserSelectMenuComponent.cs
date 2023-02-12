using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions.Components.SelectMenuComponents;

/// <see href="https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-menu-structure" />
public sealed class UserSelectMenuComponent : SelectMenuComponent
{
	/// <inheritdoc cref="Component.Type" />
	public override ComponentTypes Type { get; set; } = ComponentTypes.UserSelect;
}