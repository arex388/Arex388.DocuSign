namespace Arex388.DocuSign.Extensions;

internal static class GetEnvelopeRequestExtensions {
	public static string GetEndpoint(
		this GetEnvelope.Request request,
		AuthorizationUserAccount account) => $"{account.BaseUrl}/restapi/v2.1/accounts/{account.Id}/envelopes/{request.Id}";
}