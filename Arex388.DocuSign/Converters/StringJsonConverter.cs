using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign.Converters;

internal sealed class StringJsonConverter :
	JsonConverter<string> {
	public override string? Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) {
		var value = reader.GetString();

		return !value.HasValue()
			? null
			: value;
	}

	public override void Write(
		Utf8JsonWriter writer,
		string value,
		JsonSerializerOptions options) => writer.WriteStringValue(value);
}