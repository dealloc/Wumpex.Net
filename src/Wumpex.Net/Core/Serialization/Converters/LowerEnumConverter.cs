using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Serialization.Converters;

/// <summary>
/// Provides serialization of enum values to strings and back.
/// </summary>
public class LowerEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
	/// <inheritdoc cref="JsonConverter{T}.Read" />
	public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
			return Enum.Parse<TEnum>(reader.GetString()!);

		throw new InvalidEnumArgumentException(nameof(reader.TokenType), (int)reader.TokenType, typeof(JsonTokenType));
	}

	/// <inheritdoc cref="JsonConverter{T}.Write" />
	public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
	{
		var strValue = Enum.GetName(value);

		writer.WriteStringValue(strValue?.ToLowerInvariant());
	}
}