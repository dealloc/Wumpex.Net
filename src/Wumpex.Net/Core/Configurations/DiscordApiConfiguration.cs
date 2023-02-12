namespace Wumpex.Net.Core.Configurations;

/// <summary>
/// Represents the configuration for the Wumpex.Net.Api library.
/// </summary>
public sealed class DiscordApiConfiguration
{
	/// <summary>
	/// The endpoint to which API calls will be made.
	/// </summary>
	public string Endpoint { get; set; } = "https://discord.com/api";
}
