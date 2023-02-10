using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;
using Elixus.Discord.Core.Models.Interactions.Components.SelectMenuComponents;

namespace Elixus.Discord.Core.Models.Interactions.Components;

/// <summary>
/// Message components—we'll call them "components" moving forward—are a framework for adding interactive elements to the messages your app or bot sends.
/// They're accessible, customizable, and easy to use.
/// There are several different types of components;
/// this documentation will outline the basics of this new framework and each example.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/interactions/message-components" />
[JsonDerivedType(typeof(ActionRowComponent), (int)ComponentTypes.ActionRow)]
[JsonDerivedType(typeof(ButtonComponent), (int)ComponentTypes.Button)]
[JsonDerivedType(typeof(StringSelectMenuComponent), (int)ComponentTypes.StringSelect)]
[JsonDerivedType(typeof(UserSelectMenuComponent), (int)ComponentTypes.UserSelect)]
[JsonDerivedType(typeof(RoleSelectMenuComponent), (int)ComponentTypes.RoleSelect)]
[JsonDerivedType(typeof(MentionableSelectMenuComponent), (int)ComponentTypes.MentionableSelect)]
[JsonDerivedType(typeof(ChannelSelectMenuComponent), (int)ComponentTypes.ChannelSelect)]
[JsonDerivedType(typeof(TextInputComponent), (int)ComponentTypes.TextInput)]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
public abstract class Component
{
	/// <summary>
	/// The type of <see cref="Component" />.
	/// </summary>
	[JsonPropertyName("type")]
	public virtual ComponentTypes Type { get; set; } = ComponentTypes.ActionRow;
}