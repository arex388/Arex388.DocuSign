using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeNotificationExpiration object.
/// </summary>
public sealed class EnvelopeNotificationExpiration {
	/// <summary>
	/// The number of days before the envelope is expired.
	/// </summary>
	[JsonPropertyName("expireAfter")]
	public int After { get; init; }

	/// <summary>
	/// Flag indicating if expiration is enabled.
	/// </summary>
	[JsonPropertyName("expireEnabled")]
	public bool IsEnabled { get; init; }

	/// <summary>
	/// The number of days before sending a waning email of a pending expiration. Use 0 (zero) for no warning email.
	/// </summary>
	[JsonPropertyName("expireWarn")]
	public int WarnAfter { get; init; }
}