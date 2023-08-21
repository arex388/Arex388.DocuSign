namespace Arex388.DocuSign.Extensions;

internal static class CreateEnvelopeRequestExtensions {
	public static string GetEndpoint(
		this CreateEnvelope.Request request,
		AuthorizationUserAccount account) => $"{account.BaseUrl}/restapi/v2.1/accounts/{account.Id}/envelopes";
}