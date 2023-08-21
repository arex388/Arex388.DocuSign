namespace Arex388.DocuSign;

/// <summary>
/// DocuSign API client factory for applications integrating with more than one DocuSign account.
/// </summary>
public interface IDocuSignClientFactory {
	/// <summary>
	/// Create and cache an instance of the DocuSign API client.
	/// </summary>
	/// <param name="integrationKey">Your DocuSign integration key. The value will be used as the cache identifier.</param>
	/// <param name="userId">Your DocuSign user's id.</param>
	/// <param name="publicKey">Your DocuSign public encryption key.</param>
	/// <param name="privateKey">Your DocuSign private encryption key.</param>
	/// <param name="isProduction">Flag indicating if the client will use development or production endpoints.</param>
	/// <returns>A new or cached instance of <c>DocuSignClient</c>.</returns>
	IDocuSignClient CreateClient(
		Guid integrationKey,
		Guid userId,
		string publicKey,
		string privateKey,
		bool isProduction = false);
}