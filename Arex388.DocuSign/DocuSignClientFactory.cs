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
	/// <param name="options">DocuSign options container.</param>
	/// <returns>A new or cached instance of <c>DocuSignClient</c>.</returns>
	public IDocuSignClient CreateClient(
		DocuSignClientOptions options) {
		var key = $"{nameof(Arex388)}.{nameof(DocuSign)}.Key[{options.IntegrationKey}]";

		if (_memoryCache.TryGetValue(key, out IDocuSignClient? docuSignClient)
			&& docuSignClient is not null) {
			return docuSignClient;
		}

		docuSignClient = new DocuSignClient(_httpClient, _memoryCache, options);

		_memoryCache.Set(key, docuSignClient, _memoryCacheEntryOptions);

		return docuSignClient;
	}

	//	https://stackoverflow.com/questions/45597057/how-to-retrieve-a-list-of-memory-cache-keys-in-asp-net-core
}