using System.Text.Json.Serialization;
using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Gateway;
using Wumpex.Net.Core.Models.Guilds;
using Wumpex.Net.Core.Models.Stages;
using Wumpex.Net.Core.Models.Voice;

namespace Wumpex.Net.Core.Events.Guilds;

/// <summary>
/// This event can be sent in three different scenarios:
/// 1. When a user is initially connecting, to lazily load and backfill information for all unavailable guilds sent in the Ready event. Guilds that are unavailable due to an outage will send a Guild Delete event.
/// 2. When a Guild becomes available again to the client.
/// 3. When the current user joins a new Guild.
/// </summary>
/// <see href="https://discord.com/developers/docs/topics/gateway-events#guild-create" />
[Intent(GatewayIntents.Guilds)]
public class GuildCreateEvent : Guild
{
	/// <summary>
	/// When this guild was joined at.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public DateTime JoinedAt { get; set; }

	/// <summary>
	/// true if this is considered a large guild.
	/// </summary>
	[JsonPropertyName("large")]
	public bool Large { get; set; }

	/// <summary>
	/// Total number of members in this guild.
	/// </summary>
	[JsonPropertyName("member_count")]
	public int MemberCount { get; set; }

	/// <summary>
	/// States of members currently in voice channels; lacks the guild_id key.
	/// </summary>
	[JsonPropertyName("voice_states")]
	public List<VoiceState> VoiceStates { get; set; } = new(0);

	/// <summary>
	/// Users in the guild.
	/// </summary>
	[JsonPropertyName("members")]
	public List<GuildMember> Members { get; set; } = new(0);

	/// <summary>
	/// Channels in the guild
	/// </summary>
	[JsonPropertyName("channels")]
	public List<Channel> Channels { get; set; } = new(0);

	/// <summary>
	/// All active threads in the guild that current user has permission to view.
	/// </summary>
	[JsonPropertyName("threads")]
	public List<Channel> Threads { get; set; } = new(0);

	/// <summary>
	/// Presences of the members in the guild,
	/// will only include non-offline members if the size is greater than large threshold.
	/// </summary>
	[JsonPropertyName("presences")]
	public List<PresenceUpdate> Presences { get; set; } = new(0);

	/// <summary>
	/// Stage instances in the guild.
	/// </summary>
	[JsonPropertyName("stage_instances")]
	public List<Stage> StageInstances { get; set; } = new(0);

	/// <summary>
	/// Scheduled events in the guild.
	/// </summary>
	[JsonPropertyName("guild_scheduled_events")]
	public List<GuildScheduledEvent> GuildScheduledEvents { get; set; } = new(0);
}