using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-party" />
public sealed class ActivityParty
{
	/// <summary>
	/// ID of the party.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// Used to show the party's current and maximum size.
	/// </summary>
	[JsonPropertyName("size")]
	public List<int>? Size { get; set; } = new(0);
}