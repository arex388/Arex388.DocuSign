using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign.Converters;

internal sealed class TabAnchorUnitJsonConverter :
	JsonConverter<TabAnchorUnit> {
	public override TabAnchorUnit Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"cms" => TabAnchorUnit.Centimeter,
			"inches" => TabAnchorUnit.Inches,
			"mms" => TabAnchorUnit.Millimeter,
			"pixels" => TabAnchorUnit.Pixels,
			_ => TabAnchorUnit.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		TabAnchorUnit value,
		JsonSerializerOptions options) => writer.WriteStringValue(value.ToStringFast());
}