using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Permissions;

/// <see href="https://discord.com/developers/docs/topics/permissions#role-object-role-tags-structure" />
public sealed class RoleTag
{
	/// <summary>
	/// the id of the bot this role belongs to.
	/// </summary>
	[JsonPropertyName("bot_id")]
	public string? BotId { get; set; }

	/// <summary>
	/// the id of the integration this role belongs to.
	/// </summary>
	[JsonPropertyName("integration_id")]
	public string? IntegrationId { get; set; }

	/// <summary>
	/// Whether this is the guild's Booster role.
	/// </summary>
	[JsonPropertyName("premium_subscriber")]
	public bool? PremiumSubscriber { get; set; }

	/// <summary>
	/// the id of this role's subscription sku and listing.
	/// </summary>
	[JsonPropertyName("subscription_listing_id")]
	public string? SubscriptionListingId { get; set; }

	/// <summary>
	/// whether this role is available for purchase.
	/// </summary>
	[JsonPropertyName("available_for_purchase")]
	public bool? AvailableForPurchase { get; set; }

	/// <summary>
	/// whether this role is a guild's linked role.
	/// </summary>
	[JsonPropertyName("guild_connections")]
	public bool? GuildConnections { get; set; }
}