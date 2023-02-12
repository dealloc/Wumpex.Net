using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Guilds;
using Wumpex.Net.Core.Models.Emojis;
using Wumpex.Net.Core.Models.Permissions;
using Wumpex.Net.Core.Models.Stickers;

namespace Wumpex.Net.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#guild-object" />
public class Guild : UnavailableGuild
{
	/// <summary>
	/// Guild name (2-100 characters, excluding trailing and leading whitespace).
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Icon hash.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("icon")]
	public string? Icon { get; set; }

	/// <summary>
	/// Icon hash, returned when in the template object.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("icon_hash")]
	public string? IconHash { get; set; }

	/// <summary>
	/// Splash hash.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("splash")]
	public string? Splash { get; set; }

	/// <summary>
	/// Discovery splash hash; only present for guilds with the "DISCOVERABLE" feature.
	/// </summary>
	[JsonPropertyName("discovery_splash")]
	public string? DiscoverySplash { get; set; }

	/// <summary>
	/// true if the user is the owner of the guild.
	/// </summary>
	[JsonPropertyName("owner")]
	public bool? Owner { get; set; }

	/// <summary>
	/// id of owner.
	/// </summary>
	[JsonPropertyName("owner_id")]
	public string OwnerId { get; set; } = null!;

	/// <summary>
	/// total permissions for the user in the guild (excludes overwrites)
	/// </summary>
	[JsonPropertyName("permissions")]
	public string? Permissions { get; set; }

	/// <summary>
	/// Voice region id for the guild (deprecated).
	/// </summary>
	[Obsolete("Deprecated according to the Discord documentation")]
	[JsonPropertyName("region")]
	public string? Region { get; set; }

	/// <summary>
	/// Id of afk channel.
	/// </summary>
	[JsonPropertyName("afk_channel_id")]
	public string? AfkChannelId { get; set; }

	/// <summary>
	/// afk timeout in seconds, can be set to: 60, 300, 900, 1800, 3600.
	/// </summary>
	[JsonPropertyName("afk_timeout")]
	public int AfkTimeout { get; set; }

	/// <summary>
	/// true if the server widget is enabled.
	/// </summary>
	[JsonPropertyName("widget_enabled")]
	public bool? WidgetEnabled { get; set; }

	/// <summary>
	/// the channel id that the widget will generate an invite to, or null if set to no invite.
	/// </summary>
	[JsonPropertyName("widget_channel_id")]
	public string? WidgetChannelId { get; set; }

	/// <summary>
	/// verification level required for the guild
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/guild#guild-object-verification-level" />
	[JsonPropertyName("verification_level")]
	public VerificationLevels VerificationLevel { get; set; }

	/// <summary>
	/// default message notifications level.
	/// </summary>
	[JsonPropertyName("default_message_notifications")]
	public NotificationLevels DefaultMessageNotifications { get; set; }

	/// <summary>
	/// explicit content filter level.
	/// </summary>
	[JsonPropertyName("explicit_content_filter")]
	public ExplicitContentFilterLevels ExplicitContentFilter { get; set; }

	/// <summary>
	/// Roles in the guild.
	/// </summary>
	[JsonPropertyName("roles")]
	public List<Role> Roles { get; set; } = new(0);

	/// <summary>
	/// custom guild emojis.
	/// </summary>
	[JsonPropertyName("emojis")]
	public List<Emoji> Emojis { get; set; } = new(0);

	/// <summary>
	/// enabled guild features.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/resources/guild#guild-object-guild-features" />
	[JsonPropertyName("features")]
	public List<string> Features { get; set; } = new(0);

	/// <summary>
	/// required MFA level for the guild.
	/// </summary>
	[JsonPropertyName("mfa_levels")]
	public MfaLevels MfaLevels { get; set; }

	/// <summary>
	/// application id of the guild creator if it is bot-created.
	/// </summary>
	[JsonPropertyName("application_id")]
	public string? ApplicationId { get; set; }

	/// <summary>
	/// the id of the channel where guild notices such as welcome messages and boost events are posted.
	/// </summary>
	[JsonPropertyName("system_channel_id")]
	public string? SystemChannelId { get; set; }

	/// <summary>
	/// system channel flags.
	/// </summary>
	[JsonPropertyName("system_channel_flags")]
	public SystemChannelFlags SystemChannelFlags { get; set; }

	/// <summary>
	/// the id of the channel where Community guilds can display rules and/or guidelines.
	/// </summary>
	[JsonPropertyName("rules_channel_id")]
	public string? RulesChannelId { get; set; }

	/// <summary>
	/// the maximum number of presences for the guild (null is always returned, apart from the largest of guilds).
	/// </summary>
	[JsonPropertyName("max_presences")]
	public int? MaxPresences { get; set; }

	/// <summary>
	/// the maximum number of members for the guild.
	/// </summary>
	[JsonPropertyName("max_members")]
	public int? MaxMembers { get; set; }

	/// <summary>
	/// the vanity url code for the guild.
	/// </summary>
	[JsonPropertyName("vanity_url_code")]
	public string? VanityUrlCode { get; set; }

	/// <summary>
	/// The description of a guild.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Banner hash
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/reference#image-formatting" />
	[JsonPropertyName("banner")]
	public string? Banner { get; set; }

	/// <summary>
	/// premium tier (Server Boost level).
	/// </summary>
	[JsonPropertyName("premium_tier")]
	public PremiumTiers PremiumTier { get; set; }

	/// <summary>
	/// the number of boosts this guild currently has.
	/// </summary>
	[JsonPropertyName("premium_subscription_count")]
	public int? PremiumSubscriptionCount { get; set; }

	/// <summary>
	/// The preferred locale of a Community guild;
	/// used in server discovery and notices from Discord, and sent in interactions;
	/// defaults to "en-US".
	/// </summary>
	[JsonPropertyName("preferred_locale")]
	public string PreferredLocale { get; set; } = null!;

	/// <summary>
	/// the id of the channel where admins and moderators of Community guilds receive notices from Discord.
	/// </summary>
	[JsonPropertyName("public_updates_channel_id")]
	public string? PublicUpdatesChannelId { get; set; }

	/// <summary>
	/// the maximum amount of users in a video channel.
	/// </summary>
	[JsonPropertyName("max_video_channel_users")]
	public int? MaxVideoChannelUsers { get; set; }

	/// <summary>
	/// approximate number of members in this guild,
	/// returned from the GET /guilds/{id} endpoint when with_counts is true.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public int? ApproximateMemberCount { get; set; }

	/// <summary>
	/// 	approximate number of non-offline members in this guild,
	/// returned from the GET /guilds/{id} endpoint when with_counts is true
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public int? ApproximatePresenceCount { get; set; }

	/// <summary>
	/// the welcome screen of a Community guild, shown to new members, returned in an Invite's guild object.
	/// </summary>
	[JsonPropertyName("welcome_screen")]
	public WelcomeScreen? WelcomeScreen { get; set; }

	/// <summary>
	/// guild NSFW level.
	/// </summary>
	[JsonPropertyName("nsfw_level")]
	public NsfwLevels NsfwLevel { get; set; }

	/// <summary>
	/// custom guild stickers.
	/// </summary>
	[JsonPropertyName("stickers")]
	public List<Sticker> Stickers { get; set; } = new(0);

	/// <summary>
	/// whether the guild has the boost progress bar enabled.
	/// </summary>
	[JsonPropertyName("premium_progress_bar_enabled")]
	public bool PremiumProgressBarEnabled { get; set; }
}