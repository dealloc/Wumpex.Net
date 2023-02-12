using System.Text.Json.Serialization;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Serialization.Contexts;

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
