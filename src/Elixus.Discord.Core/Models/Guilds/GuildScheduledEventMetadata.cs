using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-entity-metadata" />
public sealed class GuildScheduledEventMetadata
{
	/// <summary>
	/// Location of the event (1-100 characters).
	/// </summary>
	[JsonPropertyName("location")]
	public string? Location { get; set; }
}