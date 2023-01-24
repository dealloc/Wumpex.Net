using Elixus.Discord.Gateway.Events;
using System.Text.Json.Serialization;

namespace Elixus.Discord.Gateway.Parsing;

/// <summary>
/// Entrypoint for the System.Text.Json serialization code generators.
/// </summary>
/// <see href="https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/" />
[JsonSerializable(typeof(HelloEvent))]
[JsonSerializable(typeof(HeartbeatEvent))]
[JsonSerializable(typeof(ReconnectEvent))]
[JsonSerializable(typeof(InvalidSessionEvent))]
internal partial class GatewayEventSerializerContext : JsonSerializerContext
{
}
