using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Interactions;

namespace Elixus.Discord.Core.Models.Interactions.InteractionResponses;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#responding-to-an-interaction" />
public sealed class InteractionResponse
{
	/// <summary>
	/// The type of response.
	/// </summary>
	[JsonPropertyName("type")]
	public InteractionCallbackTypes Type { get; set; }

	// TODO https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-data-structure
}