using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-timestamps" />
public sealed class ActivityTimestamp
{
	/// <summary>
	/// Unix time (in milliseconds) of when the activity started.
	/// </summary>
	[JsonPropertyName("start")]
	public int? Start { get; set; }

	/// <summary>
	/// Unix time (in milliseconds) of when the activity ends.
	/// </summary>
	[JsonPropertyName("end")]
	public int? End { get; set; }
}