namespace Arex388.DocuSign;

/// <summary>
/// DocuSign API client factory for applications integrating with more than one DocuSign account.
/// </summary>
public interface IDocuSignClientFactory {
	/// <summary>
	/// Create and cache an instance of the DocuSign API client.
	/// </summary>
	/// <param name="options">DocuSign options container.</param>
	/// <returns>A new or cached instance of <c>DocuSignClient</c>.</returns>
	IDocuSignClient CreateClient(
		DocuSignClientOptions options);
}