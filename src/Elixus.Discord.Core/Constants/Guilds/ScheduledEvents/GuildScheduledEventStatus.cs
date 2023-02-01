namespace Elixus.Discord.Core.Constants.Guilds.ScheduledEvents;

/// <see href="https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-status" />
public enum GuildScheduledEventStatus
{
	/// <summary>
	/// SCHEDULED
	/// </summary>
	Scheduled = 1,

	/// <summary>
	/// ACTIVE
	/// </summary>
	Active = 2,

	/// <summary>
	/// COMPLETED
	/// </summary>
	/// <remarks>
	/// Once status is set to <see cref="Completed" /> or <see cref="Cancelled" />, the status can no longer be updated.
	/// </remarks>
	Completed = 3,

	/// <summary>
	/// CANCELED
	/// </summary>
	/// <remarks>
	/// Once status is set to <see cref="Completed" /> or <see cref="Cancelled" />, the status can no longer be updated.
	/// </remarks>
	Cancelled = 4
}