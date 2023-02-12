namespace Wumpex.Net.Core.Configurations;

/// <summary>
/// Represents generic configuration required by the Wumpex.Net library.
/// </summary>
public sealed class DiscordConfiguration
{
	/// <summary>
	/// The application ID provided by Discord.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/getting-started#getting-started-with-discord-app-development" />
	public string? ApplicationId { get; set; }

	/// <summary>
	/// The bot token provided by Discord.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/getting-started#getting-started-with-discord-app-development" />
	public string? Token { get; set; }

	/// <inheritdoc cref="DiscordApiConfiguration" />
	public DiscordApiConfiguration Api { get; set; } = new();

	/// <inheritdoc cref="DiscordGatewayConfiguration" />
	public DiscordGatewayConfiguration Gateway { get; set; } = new();
}