using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Channels;

namespace Wumpex.Net.Core.Models.Interactions.ApplicationCommands.ApplicationCommandOptions;

/// <summary>
/// A subclass of <see cref="ApplicationCommand" /> with sub <see cref="ChannelTypes" />.
/// </summary>
public sealed class ApplicationCommandOptionWithChannelTypes : ApplicationCommandOption
{
	/// <summary>
	/// If the option is a channel type, the channels shown will be restricted to these types.
	/// </summary>
	[JsonPropertyName("channel_types")]
	public List<ChannelTypes> ChannelTypes { get; set; } = new(0);
}