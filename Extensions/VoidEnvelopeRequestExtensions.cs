namespace Arex388.DocuSign.Extensions;

internal static class VoidEnvelopeRequestExtensions {
	public static UpdateEnvelope.Request ToUpdateEnvelopeRequest(
		this VoidEnvelope.Request request) => new() {
			Id = request.Id,
			Status = EnvelopeStatus.Voided,
			VoidedReason = request.Reason
		};
}