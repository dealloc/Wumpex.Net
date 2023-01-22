using System.Text.Json.Serialization;

namespace Elixus.Discord.Api.Models.Gateway;

/// <summary>
/// Represents the session start limit from the <see cref="GatewayBotResponse" />.
/// </summary>
/// <seealso href="https://discord.com/developers/docs/topics/gateway#session-start-limit-object" />
public sealed class SessionStartLimit
{
	/// <summary>
	/// Total number of session starts the current user is allowed
	/// </summary>
	public int Total { get; init; }

	/// <summary>
	/// Remaining number of session starts the current user is allowed
	/// </summary>
	public int Remaining { get; init; }

	/// <summary>
	/// Number of milliseconds after which the limit resets
	/// </summary>

	[JsonPropertyName("reset_after")]
	public int ResetAfter { get; init; }

	/// <summary>
	/// Number of identify requests allowed per 5 seconds
	/// </summary>

	[JsonPropertyName("max_concurrency")]
	public int MaxConcurrency { get; init; }
}
