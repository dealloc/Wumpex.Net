using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Stages;

namespace Wumpex.Net.Core.Models.Stages;

/// <summary>
/// A Stage Instance holds information about a live stage.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/stage-instance#stage-instance-object" />
public sealed class Stage
{
	/// <summary>
	/// The id of this Stage instance.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The guild id of the associated <see cref="ChannelId" />.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; } = null!;

	/// <summary>
	/// The id of the associated Stage channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; } = null!;

	/// <summary>
	/// The topic of the Stage instance (1-120 characters).
	/// </summary>
	[JsonPropertyName("topic")]
	public string Topic { get; set; } = null!;

	/// <summary>
	/// The privacy level of the Stage instance
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public StagePrivacyLevel PrivacyLevel { get; set; }

	/// <summary>
	/// Whether or not Stage Discovery is disabled (deprecated).
	/// </summary>
	[Obsolete("Deprecated according to the Discord documentation")]
	[JsonPropertyName("discoverable_disabled")]
	public bool DiscoverableDisabled { get; set; }

	/// <summary>
	/// The id of the scheduled event for this Stage instance.
	/// </summary>
	[JsonPropertyName("guild_scheduled_event_id")]
	public string? GuildScheduledEventId { get; set; }
}