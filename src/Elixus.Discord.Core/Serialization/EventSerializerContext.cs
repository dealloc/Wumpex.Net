using Elixus.Discord.Core.Events.Gateway;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Elixus.Discord.Core.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for events.
/// </summary>
[JsonSerializable(typeof(ReadyEvent))]
[JsonSourceGenerationOptions()]
public partial class EventSerializerContext : JsonSerializerContext
{
}
