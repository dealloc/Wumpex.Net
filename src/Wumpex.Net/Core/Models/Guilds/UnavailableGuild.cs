using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Guilds;

/// <summary>
/// A partial guild object.
/// Represents an Offline Guild, or a Guild whose information has not been provided through Guild Create events during the Gateway connect.
/// </summary>
/// <see href="https://discord.com/developers/docs/resources/guild#unavailable-guild-object" />
public class UnavailableGuild
{
	/// <summary>
	/// Guild id
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// Whether or not the guild is unavailable.
	/// This is <c>true</c> for <see cref="UnavailableGuild" />.
	/// </summary>
	/// <see href="https://discord.com/developers/docs/resources/guild#unavailable-guild-object" />
	[JsonPropertyName("unavailable")]
	public virtual bool Unavailable { get; } = true;
}
