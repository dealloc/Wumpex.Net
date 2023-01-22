namespace Elixus.Discord.Api.Configurations;

/// <summary>
/// Represents the configuration for the Elixus.Discord.Api library.
/// </summary>
public sealed class DiscordApiConfiguration
{
	/// <summary>
	/// The endpoint to which API calls will be made.
	/// </summary>
	public string Endpoint { get; set; } = "https://discord.com/api";

	/// <summary>
	/// The bot token provided by Discord.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/getting-started#getting-started-with-discord-app-development" />
	public string? Token { get; set; }
}
