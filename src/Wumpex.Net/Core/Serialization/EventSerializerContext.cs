using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Wumpex.Net.Core.Events.Gateway;
using Wumpex.Net.Core.Events.Guilds;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Events.Messages;

namespace Wumpex.Net.Core.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for events.
/// </summary>
[JsonSourceGenerationOptions]
[JsonSerializable(typeof(ReadyEvent))]
[JsonSerializable(typeof(GuildCreateEvent))]
[JsonSerializable(typeof(GuildDeleteEvent))]
[JsonSerializable(typeof(MessageCreateEvent))]
[JsonSerializable(typeof(MessageDeleteEvent))]
[JsonSerializable(typeof(InteractionCreateEvent))]
public partial class EventSerializerContext : JsonSerializerContext
{
}
