using System.Text.Json.Serialization;
using Elixus.Discord.Core.Models.Interactions.ApplicationCommands;

namespace Elixus.Discord.Api.Serialization;

[JsonSerializable(typeof(ApplicationCommand))]
public partial class ApiSerializerContext : JsonSerializerContext
{
	
}