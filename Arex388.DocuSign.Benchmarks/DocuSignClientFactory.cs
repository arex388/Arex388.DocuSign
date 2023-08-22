using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;

namespace Arex388.DocuSign.Benchmarks;

[MemoryDiagnoser]
public class DocuSignClientFactory {
	private readonly IDocuSignClientFactory _docuSignFactory = new DocuSign.DocuSignClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));

	[Benchmark]
	public void CreateAndCacheClient() {
		var client = _docuSignFactory.CreateClient(new DocuSignClientOptions {
			IntegrationKey = Config.IntegrationKey,
			PrivateKey = Config.PrivateKey,
			PublicKey = Config.PublicKey,
			UserId = Config.UserId
		});

		_ = client.ToString();
	}
}