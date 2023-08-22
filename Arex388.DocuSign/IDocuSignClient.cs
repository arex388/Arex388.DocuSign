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
	public Task<CreateEnvelope.Response> CreateEnvelopeAsync(
		CreateEnvelope.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Update an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>UpdateEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	public Task<UpdateEnvelope.Response> UpdateEnvelopeAsync(
		UpdateEnvelope.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Void an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>VoidEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	public Task<UpdateEnvelope.Response> VoidEnvelopeAsync(
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