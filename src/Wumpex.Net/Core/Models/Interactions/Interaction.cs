using System.Text.Json.Serialization;
using Wumpex.Net.Core.Constants.Interactions;
using Wumpex.Net.Core.Models.Channels;
using Wumpex.Net.Core.Models.Guilds;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Models.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure" />
[JsonDerivedType(typeof(ApplicationCommandInteraction), (int)InteractionTypes.ApplicationCommand)]
[JsonDerivedType(typeof(ApplicationCommandAutocompleteInteraction), (int)InteractionTypes.ApplicationCommandAutocomplete)]
[JsonDerivedType(typeof(MessageComponentInteraction), (int)InteractionTypes.MessageComponent)]
[JsonDerivedType(typeof(ModalSubmitInteraction), (int)InteractionTypes.ModalSubmit)]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
public abstract class Interaction
{
	/// <summary>
	/// ID of the interaction.
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = null!;

	/// <summary>
	/// ID of the application this interaction is for.
	/// </summary>
	[JsonPropertyName("application_id")]
	public string ApplicationId { get; set; } = null!;

	/// <summary>
	/// Type of interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public virtual InteractionTypes Type { get; set; } = InteractionTypes.Ping;

	// Data is delegated to sub classes for polymorphic serialization.

	/// <summary>
	/// Guild that the interaction was sent from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public string? GuildId { get; set; }

	/// <summary>
	/// Channel that the interaction was sent from.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public string? ChannelId { get; set; }

	/// <summary>
	/// Guild member data for the invoking user, including permissions.
	/// </summary>
	/// <remarks>
	/// <see cref="Member" /> is sent when the interaction is invoked in a guild, and <see cref="User" /> is sent when invoked in a DM.
	/// </remarks>
	[JsonPropertyName("member")]
	public GuildMember? Member { get; set; }

	/// <summary>
	/// User object for the invoking user, if invoked in a DM.
	/// </summary>
	/// <remarks>
	/// <see cref="Member" /> is sent when the interaction is invoked in a guild, and <see cref="User" /> is sent when invoked in a DM.
	/// </remarks>
	[JsonPropertyName("user")]
	public User? User { get; set; }

	/// <summary>
	/// Continuation token for responding to the interaction.
	/// </summary>
	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	/// <summary>
	/// Read-only property, always 1.
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; }

	/// <summary>
	/// For components, the message they were attached to.
	/// </summary>
	[JsonPropertyName("message")]
	public Message? Message { get; set; }

	/// <summary>
	/// Bitwise set of permissions the app or bot has within the channel the interaction was sent from.
	/// </summary>
	[JsonPropertyName("app_permissions")]
	public string? AppPermissions { get; set; }

	/// <summary>
	/// Selected language of the invoking user.
	/// </summary>
	/// <remarks>
	/// This is available on all interaction types except PING
	/// </remarks>
	/// <seealso href="https://discord.com/developers/docs/reference#locales" />
	[JsonPropertyName("locale")]
	public string? Locale { get; set; }

	/// <summary>
	/// Guild's preferred locale, if invoked in a guild.
	/// </summary>
	[JsonPropertyName("guild_locale")]
	public string GuildLocale { get; set; } = null!;
}

/// <summary>
/// A <see cref="Interaction" /> for <see cref="InteractionTypes.ApplicationCommand" />
/// </summary>
public class ApplicationCommandInteraction : Interaction
{
	/// <inheritdoc cref="Interaction.Type" />
	public override InteractionTypes Type { get; set; } = InteractionTypes.ApplicationCommand;

	/// <summary>
	/// Interaction data payload.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data" />
	[JsonPropertyName("data")]
	public ApplicationCommandData? Data { get; set; }
}

/// <summary>
/// A <see cref="Interaction" /> for <see cref="InteractionTypes.ApplicationCommandAutocomplete" />
/// </summary>
public sealed class ApplicationCommandAutocompleteInteraction : ApplicationCommandInteraction
{
	/// <inheritdoc cref="Interaction.Type" />
	public override InteractionTypes Type { get; set; } = InteractionTypes.ApplicationCommandAutocomplete;
}

/// <summary>
/// A <see cref="Interaction" /> for <see cref="InteractionTypes.MessageComponent" />
/// </summary>
public sealed class MessageComponentInteraction : Interaction
{
	/// <inheritdoc cref="Interaction.Type" />
	public override InteractionTypes Type { get; set; } = InteractionTypes.MessageComponent;

	/// <summary>
	/// Interaction data payload.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data" />
	[JsonPropertyName("data")]
	public MessageComponentData? Data { get; set; }
}

/// <summary>
/// A <see cref="Interaction" /> for <see cref="InteractionTypes.ModalSubmit" />
/// </summary>
public sealed class ModalSubmitInteraction : Interaction
{
	/// <inheritdoc cref="Interaction.Type" />
	public override InteractionTypes Type { get; set; } = InteractionTypes.ModalSubmit;

	/// <summary>
	/// Interaction data payload.
	/// </summary>
	/// <seealso href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data" />
	[JsonPropertyName("data")]
	public ModalSubmitData? Data { get; set; }
}
