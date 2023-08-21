using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign.Converters;

internal sealed class EnvelopeDocumentDisplayJsonConverter :
	JsonConverter<EnvelopeDocumentDisplay> {
	public override EnvelopeDocumentDisplay Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"inline" => EnvelopeDocumentDisplay.Inline,
			"modal" => EnvelopeDocumentDisplay.Modal,
			_ => EnvelopeDocumentDisplay.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		EnvelopeDocumentDisplay value,
		JsonSerializerOptions options) => writer.WriteStringValue(value.ToStringFast());
}