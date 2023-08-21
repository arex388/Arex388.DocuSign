using Arex388.DocuSign.Converters;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeDocument object.
/// </summary>
public sealed class EnvelopeDocument {
	/// <summary>
	/// The document's base64 encoded representation.
	/// </summary>
	[JsonPropertyName("documentBase64")]
	public string Base64 { get; init; } = null!;

	/// <summary>
	/// The document's display mode. Inline by default.
	/// </summary>
	[JsonConverter(typeof(EnvelopeDocumentDisplayJsonConverter))]
	public EnvelopeDocumentDisplay Display { get; init; } = EnvelopeDocumentDisplay.Inline;

	/// <summary>
	/// The document's file extension.
	/// </summary>
	[JsonPropertyName("fileExtension")]
	public string Extension { get; init; } = null!;

	/// <summary>
	/// The document's id in the context of the envelope.
	/// </summary>
	[JsonPropertyName("documentId")]
	public int Id { get; init; }

	/// <summary>
	/// The document's name.
	/// </summary>
	public string Name { get; init; } = null!;
}