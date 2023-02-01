namespace Elixus.Discord.Core.Constants.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway#list-of-intents" />
/// <seealso href="https://discord-intents-calculator.vercel.app/" />
[Flags]
public enum GatewayIntents
{
	/// <summary>
	/// No specific intents, just the default events.
	/// - READY
	/// - RESUMED
	/// - VOICE_SERVER_UPDATE
	/// - USER_UPDATE
	/// - INTERACTION_CREATE
	/// </summary>
	Default = 0,

	/// <summary>
	/// GUILDS
	/// </summary>
	Guilds = 1 << 0,

	/// <summary>
	/// GUILD_MEMBERS
	/// </summary>
	GuildMembers = 1 << 1,

	/// <summary>
	/// GUILD_MODERATION
	/// </summary>
	GuildModeration = 1 << 2,

	/// <summary>
	/// GUILD_EMOJIS_AND_STICKERS
	/// </summary>
	GuildEmojisAndStickers = 1 << 3,

	/// <summary>
	/// GUILD_INTEGRATIONS
	/// </summary>
	GuildIntegrations = 1 << 4,

	/// <summary>
	/// GUILD_WEBHOOKS
	/// </summary>
	GuildWebhooks = 1 << 5,

	/// <summary>
	/// GUILD_INVITES
	/// </summary>
	GuildInvites = 1 << 6,

	/// <summary>
	/// GUILD_VOICE_STATES
	/// </summary>
	GuildVoiceStates = 1 << 7,

	/// <summary>
	/// GUILD_PRESENCES
	/// </summary>
	GuildPresences = 1 << 8,

	/// <summary>
	/// GUILD_MESSAGES
	/// </summary>
	GuildMessages = 1 << 9,

	/// <summary>
	/// GUILD_MESSAGE_REACTIONS
	/// </summary>
	GuildMessageReactions = 1 << 10,

	/// <summary>
	/// GUILD_MESSAGE_TYPING
	/// </summary>
	GuildMessageTyping = 1 << 11,

	/// <summary>
	/// DIRECT_MESSAGES
	/// </summary>
	DirectMessages = 1 << 12,

	/// <summary>
	/// DIRECT_MESSAGE_REACTIONS
	/// </summary>
	DirectMessageReactions = 1 << 13,

	/// <summary>
	/// DIRECT_MESSAGE_TYPING
	/// </summary>
	DirectMessageTyping = 1 << 14,

	/// <summary>
	/// MESSAGE_CONTENT
	/// </summary>
	MessageContent = 1 << 15,

	/// <summary>
	/// GUILD_SCHEDULED_EVENTS
	/// </summary>
	GuildScheduledEvents = 1 << 16,

	/// <summary>
	/// AUTO_MODERATION_CONFIGURATION
	/// </summary>
	AutoModerationConfiguration = 1 << 20,

	/// <summary>
	/// AUTO_MODERATION_EXECUTION
	/// </summary>
	AutoModerationExecution = 1 << 21
}