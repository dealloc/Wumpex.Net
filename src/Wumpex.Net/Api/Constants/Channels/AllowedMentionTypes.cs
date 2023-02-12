using System.Text.Json.Serialization;
using Wumpex.Net.Core.Serialization.Converters;

namespace Wumpex.Net.Api.Constants.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types" />
[JsonConverter(typeof(LowerEnumConverter<AllowedMentionTypes>))]
public enum AllowedMentionTypes
{
	/// <summary>
	/// Controls role mentions.
	/// </summary>
	Roles,

	/// <summary>
	/// Controls user mentions.
	/// </summary>
	Users,

	/// <summary>
	/// Controls @everyone and @here mentions
	/// </summary>
	Everyone,
}