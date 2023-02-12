namespace Wumpex.Net.Core.Constants.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-object-default-message-notification-level" />
public enum NotificationLevels
{
	/// <summary>
	/// members will receive notifications for all messages by default.
	/// </summary>
	AllMessages = 0,

	/// <summary>
	/// members will receive notifications only for messages that @mention them by default.
	/// </summary>
	OnlyMentions = 1
}