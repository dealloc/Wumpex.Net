using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Models.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#attachment-object" />
public class Attachment
{
	/// <summary>
	/// attachment id.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// Name of file attached.
	/// </summary>
	[JsonPropertyName("filename")]
	public string Filename { get; set; } = null!;

	/// <summary>
	/// Description for the file (max 1024 characters).
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// The attachment's media type.
	/// </summary>
	[JsonPropertyName("content_type")]
	public string? ContentType { get; set; }

	/// <summary>
	/// Size of file in bytes.
	/// </summary>
	[JsonPropertyName("size")]
	public int Size { get; set; }

	/// <summary>
	/// Source url of file.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = null!;

	/// <summary>
	/// A proxied url of file.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public string ProxyUrl { get; set; } = null!;

	/// <summary>
	/// Height of file (if image).
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; set; }

	/// <summary>
	/// Width of file (if image).
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; set; }

	/// <summary>
	/// Whether this attachment is ephemeral.
	/// </summary>
	/// <remarks>
	/// Ephemeral attachments will automatically be removed after a set period of time.
	/// Ephemeral attachments on messages are guaranteed to be available as long as the message itself exists.
	/// </remarks>
	[JsonPropertyName("ephemeral")]
	public bool? Ephemeral { get; set; }
}