using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Guilds.ScheduledEvents;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object" />
public sealed class GuildScheduledEvent
{
	/// <summary>
	/// the id of the scheduled event.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The guild id which the scheduled event belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; } = null!;

	/// <summary>
	/// the channel id in which the scheduled event will be hosted,
	/// or null if <see cref="EntityType" /> is <see cref="GuildScheduledEventEntityTypes.External" />.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string? ChannelId { get; set; }

	/// <summary>
	/// the id of the user that created the scheduled event.
	/// </summary>
	/// <remarks>
	/// creator_id will be null and creator will not be included for events created before October 25th, 2021,
	/// when the concept of creator_id was introduced and tracked.
	/// </remarks>
	[JsonPropertyName("creator_id")]
	public string? CreatorId { get; set; }

	/// <summary>
	/// The name of the scheduled event (1-100 characters).
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// the description of the scheduled event (1-1000 characters).
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// the time the scheduled event will start.
	/// </summary>
	[JsonPropertyName("scheduled_start_time")]
	public DateTime ScheduledStartTime { get; set; }

	/// <summary>
	/// The time the scheduled event will end,
	/// required if <see cref="EntityType" /> is <see cref="GuildScheduledEventEntityTypes.External" />.
	/// </summary>
	/// <remarks>
	/// See field requirements by entity type to understand the relationship between entity_type and the following fields: channel_id, entity_metadata, and scheduled_end_time
	/// </remarks>
	/// <seealso href="https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-field-requirements-by-entity-type" />
	[JsonPropertyName("scheduled_end_time")]
	public DateTime? ScheduledEndTime { get; set; }

	/// <summary>
	/// The privacy level of the scheduled event.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public GuildScheduledEventPrivacyLevels PrivacyLevel { get; set; }

	/// <summary>
	/// The status of the scheduled event.
	/// </summary>
	[JsonPropertyName("status")]
	public GuildScheduledEventStatus Status { get; set; }

	/// <summary>
	/// The type of the scheduled event.
	/// </summary>
	[JsonPropertyName("entity_type")]
	public GuildScheduledEventEntityTypes EntityType { get; set; }

	/// <summary>
	/// The id of an entity associated with a guild scheduled event.
	/// </summary>
	[JsonPropertyName("entity_id")]
	public string? EntityId { get; set; }

	/// <summary>
	/// Additional metadata for the guild scheduled event.
	/// </summary>
	[JsonPropertyName("entity_metadata")]
	public GuildScheduledEventMetadata? EntityMetadata { get; set; }

	/// <summary>
	/// The user that created the scheduled event.
	/// </summary>
	[JsonPropertyName("creator")]
	public User? Creator { get; set; }

	/// <summary>
	/// The number of users subscribed to the scheduled event.
	/// </summary>
	[JsonPropertyName("user_count")]
	public int? UserCount { get; set; }

	/// <summary>
	/// the cover image hash of the scheduled event.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("image")]
	public string? Image { get; set; }
}