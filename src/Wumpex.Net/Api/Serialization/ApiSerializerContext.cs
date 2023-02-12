using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Wumpex.Net.Api.Models.Channels;
using Wumpex.Net.Api.Models.Interactions.InteractionResponses;
using Wumpex.Net.Core.Models.Interactions.ApplicationCommands;

namespace Wumpex.Net.Api.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for requests/responses.
/// </summary>
[JsonSerializable(typeof(ApplicationCommand))]
[JsonSerializable(typeof(CreateMessageRequest))]
[JsonSerializable(typeof(InteractionResponse))]
public partial class ApiSerializerContext : JsonSerializerContext
{
	
}