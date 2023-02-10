using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Api.Models.Channels;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

namespace Elixus.Discord.Api.Serialization;

/// <summary>
/// Helper class that provides <see cref="JsonTypeInfo{T}" /> for requests/responses.
/// </summary>
[JsonSerializable(typeof(ApplicationCommand))]
[JsonSerializable(typeof(CreateMessageRequest))]
public partial class ApiSerializerContext : JsonSerializerContext
{
	
}