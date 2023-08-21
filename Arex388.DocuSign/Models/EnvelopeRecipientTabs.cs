namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeRecipientTabs object.
/// </summary>
public sealed class EnvelopeRecipientTabs {
	/// <summary>
	/// All signature tabs for the envelope.
	/// </summary>
	public IEnumerable<EnvelopeRecipientTab> SignHereTabs { get; init; } = Enumerable.Empty<EnvelopeRecipientTab>();
}