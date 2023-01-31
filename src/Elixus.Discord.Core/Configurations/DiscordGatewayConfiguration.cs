using Elixus.Discord.Core.Constants;

namespace Elixus.Discord.Core.Configurations;

/// <summary>
/// Represents the configuration for the Elixus.Discord.Gateway library.
/// </summary>
public sealed class DiscordGatewayConfiguration
{
	/// <summary>
	/// Which <see cref="GatewayIntents" /> to request from the gateway.
	/// This directly controls which events Discord will sent to your bot.
	/// </summary>
	public GatewayIntents Intents { get; set; } = GatewayIntents.Default;
}