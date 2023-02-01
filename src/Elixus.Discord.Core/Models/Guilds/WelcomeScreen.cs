using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Guilds;

/// <see href="https://discord.com/developers/docs/resources/guild#welcome-screen-object" />
public sealed class WelcomeScreen
{
	/// <summary>
	/// the server description shown in the welcome screen.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// the channels shown in the welcome screen, up to 5.
	/// </summary>
	[JsonPropertyName("welcome_channels")]
	public List<WelcomeScreenChannel> WelcomeChannels { get; set; } = new(0);
}