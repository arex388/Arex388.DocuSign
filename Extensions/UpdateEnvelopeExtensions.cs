namespace Arex388.DocuSign.Extensions;

internal static class UpdateEnvelopeExtensions {
	public static string GetEndpoint(
		this UpdateEnvelope.Request request,
		AuthorizationUserAccount account) => $"{account.BaseUrl}/restapi/v2.1/accounts/{account.Id}/envelopes/{request.Id}";
}