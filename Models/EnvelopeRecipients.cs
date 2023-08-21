namespace Arex388.DocuSign;

/// <summary>
/// Envelope recipients container.
/// </summary>
public sealed class EnvelopeRecipients {
	/// <summary>
	/// List of signers for the envelope.
	/// </summary>
	public IEnumerable<EnvelopeRecipient> Signers { get; init; } = Enumerable.Empty<EnvelopeRecipient>();
}