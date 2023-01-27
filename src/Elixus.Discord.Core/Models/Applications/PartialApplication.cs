using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Applications;

/// <see href="https://discord.com/developers/docs/resources/application#application-object" />
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#ready" />
public class PartialApplication
{
	/// <summary>
	/// The id of the app
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// The application's public flags
	/// </summary>
	[JsonPropertyName("flags")]
	public int? Flags { get; set; }
}
