using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Applications;

namespace Wumpex.Net.Core.Models.Applications;

/// <see href="https://discord.com/developers/docs/resources/application#application-object" />
/// <seealso href="https://discord.com/developers/docs/topics/gateway-events#ready" />
public sealed class PartialApplication
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
	public ApplicationFlags? Flags { get; set; }
}
