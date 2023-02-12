using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wumpex.Net.Core.Serialization.Converters;

/// <summary>
/// Allows reading (and writing) both numbers and strings as string fields.
/// </summary>
public class NumberOrStringConverter : JsonConverter<string>
{
	/// <inheritdoc cref="JsonConverter{T}.Read" />
	public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.None => throw new InvalidEnumArgumentException("Unable to convert None to string"),
			JsonTokenType.StartObject => throw new InvalidEnumArgumentException("Unable to convert StartObject to string"),
			JsonTokenType.EndObject => throw new InvalidEnumArgumentException("Unable to convert EndObject to string"),
			JsonTokenType.StartArray => throw new InvalidEnumArgumentException("Unable to convert StartArray to string"),
			JsonTokenType.EndArray => throw new InvalidEnumArgumentException("Unable to convert EndArray to string"),
			JsonTokenType.PropertyName => reader.GetString(),
			JsonTokenType.Comment => throw new InvalidEnumArgumentException("Unable to convert Comment to string"),
			JsonTokenType.String => reader.GetString(),
			JsonTokenType.Number => reader.GetInt64().ToString(),
			JsonTokenType.True => "true",
			JsonTokenType.False => "false",
			JsonTokenType.Null => null,
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	/// <inheritdoc cref="JsonConverter{T}.Write" />
	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value);
	}
}