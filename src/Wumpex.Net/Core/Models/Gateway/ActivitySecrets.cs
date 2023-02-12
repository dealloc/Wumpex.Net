using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-secrets" />
public sealed class ActivitySecrets
{
	/// <summary>
	/// Secret for joining a party
	/// </summary>
	[JsonPropertyName("join")]
	public string? Join { get; set; }

	/// <summary>
	/// Secret for spectating a game.
	/// </summary>
	[JsonPropertyName("spectate")]
	public string? Spectate { get; set; }

	/// <summary>
	/// Secret for a specific instanced match.
	/// </summary>
	[JsonPropertyName("match")]
	public string? Match { get; set; }
}