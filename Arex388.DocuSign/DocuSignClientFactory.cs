using Microsoft.Extensions.Caching.Memory;

namespace Arex388.DocuSign;

/// <summary>
/// DocuSign API client factory for applications integrating with more than one DocuSign account.
/// </summary>
public sealed class DocuSignClientFactory :
	IDocuSignClientFactory {
	private static readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new() {
		SlidingExpiration = TimeSpan.MaxValue
	};

	private readonly HttpClient _httpClient;
	private readonly IMemoryCache _memoryCache;

	/// <summary>
	/// Create an instance of the DocuSign API client factory.
	/// </summary>
	/// <param name="httpClient">An instance of <c>HttpClient</c>.</param>
	/// <param name="memoryCache">An instance of <c>MemoryCache</c>.</param>
	public DocuSignClientFactory(
		HttpClient httpClient,
		IMemoryCache memoryCache) {
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
	}

	/// <summary>
	/// Create and cache an instance of the DocuSign API client.
	/// </summary>
	/// <param name="integrationKey">Your DocuSign integration key. The value will be used as the cache identifier.</param>
	/// <param name="userId">Your DocuSign user's id.</param>
	/// <param name="publicKey">Your DocuSign public encryption key.</param>
	/// <param name="privateKey">Your DocuSign private encryption key.</param>
	/// <param name="isProduction">Flag indicating if the client will use development or production endpoints.</param>
	/// <returns>A new or cached instance of <c>DocuSignClient</c>.</returns>
	public IDocuSignClient CreateClient(
		Guid integrationKey,
		Guid userId,
		string publicKey,
		string privateKey,
		bool isProduction = false) {
		var key = $"{nameof(Arex388)}.{nameof(DocuSign)}.Key[{integrationKey}]";

		if (_memoryCache.TryGetValue(key, out IDocuSignClient? docuSignClient)
			&& docuSignClient is not null) {
			return docuSignClient;
		}

		docuSignClient = new DocuSignClient(_httpClient, new DocuSignClientOptions {
			IntegrationKey = integrationKey,
			IsProduction = isProduction,
			PrivateKey = privateKey,
			PublicKey = publicKey,
			UserId = userId
		});

		_memoryCache.Set(key, docuSignClient, _memoryCacheEntryOptions);

		return docuSignClient;
	}
}