using System.Text.Json.Serialization;
using Elixus.Discord.Core.Models.Interactions.Components;

namespace Elixus.Discord.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-modal-submit-data-structure" />
public sealed class ModalSubmitData : InteractionData
{
	/// <summary>
	/// the custom_id of the modal.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public string CustomId { get; set; } = null!;

	/// <summary>
	/// The values submitted by the user.
	/// </summary>
	[JsonPropertyName("components")]
	public List<Component> Components { get; set; } = new(0);
}