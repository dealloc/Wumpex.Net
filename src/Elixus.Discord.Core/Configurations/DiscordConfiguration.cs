namespace Elixus.Discord.Core.Configurations;

/// <summary>
/// Represents generic configuration required by the Elixus.Discord library.
/// </summary>
public sealed class DiscordConfiguration
{

	/// <summary>
	/// The bot token provided by Discord.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/getting-started#getting-started-with-discord-app-development" />
	public string? Token { get; set; }

	/// <inheritdoc cref="DiscordApiConfiguration" />
	public DiscordApiConfiguration Api { get; set; } = new();
}