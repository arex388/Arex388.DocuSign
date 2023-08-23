using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeNotification object.
/// </summary>
public sealed class EnvelopeNotification {
	/// <summary>
	/// The envelope's expiration settings.
	/// </summary>
	[JsonPropertyName("expirations")]
	public EnvelopeNotificationExpiration? Expiration { get; init; }
}