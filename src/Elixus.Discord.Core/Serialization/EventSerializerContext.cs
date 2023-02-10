using Elixus.Discord.Core.Events.Gateway;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Core.Events.Guilds;
using Elixus.Discord.Core.Events.Interactions;
using Elixus.Discord.Core.Events.Messages;
using Elixus.Discord.Core.Models.Interactions.InteractionResponses;

namespace Elixus.Discord.Core.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for events.
/// </summary>
[JsonSourceGenerationOptions]
[JsonSerializable(typeof(ReadyEvent))]
[JsonSerializable(typeof(GuildCreateEvent))]
[JsonSerializable(typeof(MessageCreateEvent))]
[JsonSerializable(typeof(InteractionCreateEvent))]
[JsonSerializable(typeof(InteractionResponse))]
public partial class EventSerializerContext : JsonSerializerContext
{
}
