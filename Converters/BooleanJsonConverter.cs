using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign.Converters;

internal sealed class BooleanJsonConverter :
	JsonConverter<bool> {
	public override bool Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString()?.ToLowerInvariant() switch {
			"true" => true,
			_ => false
		};

	public override void Write(
		Utf8JsonWriter writer,
		bool value,
		JsonSerializerOptions options) => writer.WriteStringValue(value.ToString().ToLowerInvariant());
}