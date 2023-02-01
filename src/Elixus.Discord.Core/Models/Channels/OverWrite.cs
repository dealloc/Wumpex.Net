using System.Text.Json.Serialization;

namespace Elixus.Discord.Core.Models.Channels;

/// <summary>
/// See permissions for more information about the allow and deny fields.
/// </summary>
public sealed class OverWrite
{
	/// <summary>
	/// Role or user id.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// Either <c>role</c> or <c>member</c>
	/// </summary>
	/// <remarks>
	/// Discord documentation says it's an integer, but the gateway sends strings.
	/// </remarks>
	[JsonPropertyName("type")]
	public string Type { get; set; } = null!;

	/// <summary>
	/// Permission bit set.
	/// </summary>
	[JsonPropertyName("allow_new")]
	public string Allow { get; set; } = null!;

	/// <summary>
	/// Permission bit set.
	/// </summary>
	[JsonPropertyName("deny_new")]
	public string Deny { get; set; } = null!;
}