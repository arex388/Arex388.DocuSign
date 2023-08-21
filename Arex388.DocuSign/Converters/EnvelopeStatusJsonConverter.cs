using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign.Converters;

internal sealed class EnvelopeStatusJsonConverter :
	JsonConverter<EnvelopeStatus> {
	public override EnvelopeStatus Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"completed" => EnvelopeStatus.Completed,
			"created" => EnvelopeStatus.Created,
			"declined" => EnvelopeStatus.Declined,
			"delivered" => EnvelopeStatus.Delivered,
			"sent" => EnvelopeStatus.Sent,
			"signed" => EnvelopeStatus.Signed,
			"voided" => EnvelopeStatus.Voided,
			_ => EnvelopeStatus.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		EnvelopeStatus value,
		JsonSerializerOptions options) => writer.WriteStringValue(value.ToStringFast());
}