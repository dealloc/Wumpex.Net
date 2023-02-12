using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Activities;

namespace Wumpex.Net.Core.Models.Gateway;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object" />
public sealed class Activity
{
	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Activity type.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-types" />
	[JsonPropertyName("type")]
	public ActivityTypes Type { get; set; }

	/// <summary>
	/// Stream URL, is validated when type is 1.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Unix timestamp (in milliseconds) of when the activity was added to the user's session.
	/// </summary>
	[JsonPropertyName("created_at")]
	public int CreatedAt { get; set; }

	/// <summary>
	/// Unix timestamps for start and/or end of the game.
	/// </summary>
	[JsonPropertyName("timestamps")]
	public ActivityTimestamp? Timestamps { get; set; }

	/// <summary>
	/// Application ID for the game.
	/// </summary>
	[JsonPropertyName("application_id")]
	public string? ApplicationId { get; set; }

	/// <summary>
	/// What the player is currently doing.
	/// </summary>
	[JsonPropertyName("details")]
	public string? Details { get; set; }

	/// <summary>
	/// User's current party status.
	/// </summary>
	[JsonPropertyName("state")]
	public string? State { get; set; }

	/// <summary>
	/// Emoji used for a custom status.
	/// </summary>
	[JsonPropertyName("emoji")]
	public ActivityEmoji? Emoji { get; set; }

	/// <summary>
	/// Information for the current party of the player.
	/// </summary>
	[JsonPropertyName("party")]
	public ActivityParty? Party { get; set; }

	/// <summary>
	/// Images for the presence and their hover texts.
	/// </summary>
	public ActivityAssets? Assets { get; set; }

	/// <summary>
	/// Secrets for Rich Presence joining and spectating.
	/// </summary>
	[JsonPropertyName("secrets")]
	public ActivitySecrets? Secrets { get; set; }

	/// <summary>
	/// Whether or not the activity is an instanced game session.
	/// </summary>
	[JsonPropertyName("instance")]
	public bool? Instance { get; set; }

	/// <summary>
	/// Activity flags ORd together, describes what the payload includes.
	/// </summary>
	public ActivityFlags? Flags { get; set; }

	/// <summary>
	/// Custom buttons shown in the Rich Presence (max 2).
	/// </summary>
	[JsonPropertyName("buttons")]
	public List<ActivityButton> Buttons = new(0);
}