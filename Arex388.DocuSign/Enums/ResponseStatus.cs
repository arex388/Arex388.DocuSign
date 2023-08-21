namespace Arex388.DocuSign;

/// <summary>
/// The response's status.
/// </summary>
public enum ResponseStatus :
	byte {
	/// <summary>
	/// The request was cancelled by the client.
	/// </summary>
	Cancelled,

	/// <summary>
	/// The request failed at the service.
	/// </summary>
	Failed,

	/// <summary>
	/// The request was rejected as invalid by the client.
	/// </summary>
	Invalid,

	/// <summary>
	/// the request succeeded.
	/// </summary>
	Succeeded,

	/// <summary>
	/// The request timed out.
	/// </summary>
	TimeOut
}