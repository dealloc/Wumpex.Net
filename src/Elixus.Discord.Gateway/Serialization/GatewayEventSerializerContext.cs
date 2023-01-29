using System.Text.Json.Serialization;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Serialization;

/// <summary>
/// Entrypoint for the System.Text.Json serialization code generators.
/// </summary>
/// <see href="https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/" />
[JsonSerializable(typeof(HelloEvent))]
[JsonSerializable(typeof(ReconnectEvent))]
[JsonSerializable(typeof(InvalidSessionEvent))]
[JsonSerializable(typeof(GatewayEvent<IdentifyEvent>))]
[JsonSerializable(typeof(GatewayEvent<ResumeEvent>))]
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class GatewayEventSerializerContext : JsonSerializerContext
{
}
