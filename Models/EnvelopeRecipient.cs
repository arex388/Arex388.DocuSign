using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeRecipient object.
/// </summary>
public sealed class EnvelopeRecipient {
	/// <summary>
	/// The recipient's email.
	/// </summary>
	public string Email { get; init; } = null!;

	/// <summary>
	/// The recipient's id in the context of the envelope.
	/// </summary>
	[JsonPropertyName("recipientId")]
	public int Id { get; init; }

	/// <summary>
	/// The recipient's name.
	/// </summary>
	public string Name { get; init; } = null!;

	/// <summary>
	/// The recipient's document tabs.
	/// </summary>
	public EnvelopeRecipientTabs Tabs { get; init; } = null!;
}