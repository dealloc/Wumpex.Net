using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;

namespace Wumpex.Net.Core.Models.Interactions.Components;

/// <summary>
/// An Action Row is a non-interactive container <see cref="Component" /> for other types of <see cref="Component" />s.
/// It has a type: <see cref="ComponentTypes.ActionRow" /> and a sub-array of <see cref="Component" />s of other types.
/// </summary>
/// <remarks>
/// You can have up to 5 Action Rows per message.
/// An Action Row cannot contain another <see cref="ActionRowComponent" />.
/// </remarks>
public class ActionRowComponent<TComponent> : Component where TComponent : Component
{
	/// <summary>
	/// Contains <see cref="Component" />s.
	/// </summary>
	[JsonPropertyName("components")]
	public List<TComponent> Components { get; set; } = new(0);
}

/// <summary>
/// Default implementation of <see cref="ActionRowComponent{TComponent}" /> allowing all types of <see cref="Component" />.
/// </summary>
public class ActionRowComponent : ActionRowComponent<Component>
{
	//
}