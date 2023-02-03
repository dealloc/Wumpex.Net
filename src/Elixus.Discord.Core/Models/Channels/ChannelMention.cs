using System.Text.Json.Serialization;
using Elixus.Discord.Core.Constants.Channels;

namespace Elixus.Discord.Core.Models.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#channel-mention-object" />
public class ChannelMention
{
	/// <summary>
	/// Id of the channel.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// Id of the guild containing the channel.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; } = null!;

	/// <summary>
	/// The type of channel.
	/// </summary>
	[JsonPropertyName("type")]
	public ChannelTypes Type { get; set; }

	/// <summary>
	/// The name of the channel.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;
}