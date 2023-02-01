using Elixus.Discord.Core.Events.Gateway;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Core.Events.Guilds;

namespace Elixus.Discord.Core.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for events.
/// </summary>
[JsonSourceGenerationOptions]
[JsonSerializable(typeof(ReadyEvent))]
[JsonSerializable(typeof(GuildCreateEvent))]
public partial class EventSerializerContext : JsonSerializerContext
{
}
