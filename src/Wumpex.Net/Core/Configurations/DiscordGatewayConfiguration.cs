using Wumpex.Net.Core.Constants.Gateway;

namespace Wumpex.Net.Core.Configurations;

/// <summary>
/// Represents the configuration for the Wumpex.Net.Gateway library.
/// </summary>
public sealed class DiscordGatewayConfiguration
{
	/// <summary>
	/// Which <see cref="GatewayIntents" /> to request from the gateway.
	/// This directly controls which events Discord will sent to your bot.
	/// </summary>
	public GatewayIntents Intents { get; set; } = GatewayIntents.Default;

	/// <summary>
	/// The amount of workers to run in the background for processing dispatch events.
	/// </summary>
	public int WorkerCount { get; set; } = 5;
}