namespace Elixus.Discord.Core.Configurations;

/// <summary>
/// Represents the configuration for the Elixus.Discord.Api library.
/// </summary>
public sealed class DiscordApiConfiguration
{
	/// <summary>
	/// The endpoint to which API calls will be made.
	/// </summary>
	public string Endpoint { get; set; } = "https://discord.com/api";
}
