namespace Arex388.DocuSign;

/// <summary>
/// DocuSign API client.
/// </summary>
public interface IDocuSignClient {
	/// <summary>
	/// Create an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>CreateEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>CreateEnvelope.Response</c>.</returns>
	Task<CreateEnvelope.Response> CreateEnvelopeAsync(
		CreateEnvelope.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get the client's diagnostic information.
	/// </summary>
	object GetDiagnosticInformation();

	/// <summary>
	/// Get an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>GetEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetEnvelope.Response</c>.</returns>
	Task<GetEnvelope.Response> GetEnvelopeAsync(
		GetEnvelope.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get the user authorization URL.
	/// </summary>
	/// <param name="redirectUrl">The URL to redirect to after the user is authorized. The URL must be identical to one or more enabled redirect URLs in DocuSign.</param>
	/// <returns>The URL string.</returns>
	string GetUserAuthorizationUrl(
		string redirectUrl);

	/// <summary>
	/// Update an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>UpdateEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	Task<UpdateEnvelope.Response> UpdateEnvelopeAsync(
		UpdateEnvelope.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Void an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>VoidEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	Task<UpdateEnvelope.Response> VoidEnvelopeAsync(
		VoidEnvelope.Request request,
		CancellationToken cancellationToken = default);

	///// <summary>
	///// Returns a user.
	///// </summary>
	///// <param name="cancellationToken">The cancellation token.</param>
	///// <returns>An instance of <c>GetUser.Response</c>.</returns>
	//public Task<GetUser.Response> GetUserAsync(
	//	CancellationToken cancellationToken = default);
}