using Arex388.DocuSign.Converters;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// Envelope object.
/// </summary>
public sealed class Envelope {
	/// <summary>
	/// The envelope's id.
	/// </summary>
	[JsonPropertyName("envelopeId")]
	public Guid Id { get; init; }

	/// <summary>
	/// The envelope's status.
	/// </summary>
	[JsonConverter(typeof(EnvelopeStatusJsonConverter))]
	public EnvelopeStatus Status { get; init; }

	/// <summary>
	/// The envelope's most recent status change timestamp in UTC.
	/// </summary>
	[JsonPropertyName("statusDateTime")]
	public DateTimeOffset StatusAtUtc { get; set; }

	/// <summary>
	/// DO NOT USE THIS PROPERTY. USE <c>StatusAtUtc</c> INSTEAD. This property exists only to satisfy multiple names for the same property from DocuSign's API.
	/// </summary>
	[JsonPropertyName("statusChangedDateTime")]
	public DateTimeOffset? StatusAtUtc2 {
		init {
			if (!value.HasValue) {
				return;
			}

			StatusAtUtc = value.Value;
		}
	}

	/// <summary>
	/// The envelope's URL.
	/// </summary>
	[JsonPropertyName("uri")]
	public string Url { get; init; } = null!;
}