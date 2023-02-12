using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels.Embeds;

/// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-field-structure" />
public class EmbedField
{
	/// <summary>
	/// Name of the field.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Value of the field.
	/// </summary>
	[JsonPropertyName("value")]
	public string Value { get; set; } = null!;

	/// <summary>
	/// Whether or not this field should display inline.
	/// </summary>
	[JsonPropertyName("inline")]
	public bool? Inline { get; set; }
}