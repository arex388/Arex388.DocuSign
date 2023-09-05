using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace Arex388.DocuSign.Tests;

public sealed class DocuSignClientFactory {
	private readonly IDocuSignClientFactory _docuSignFactory = new DocuSign.DocuSignClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));

	[Fact]
	public void CreateAndCacheClient() {
		var created = _docuSignFactory.CreateClient(new DocuSignClientOptions {
			IntegrationKey = Config.IntegrationKey1,
			IsProduction = true,
			PrivateKey = Config.PrivateKey1,
			PublicKey = Config.PublicKey1,
			UserId = Config.UserId1
		});
		var cached = _docuSignFactory.CreateClient(new DocuSignClientOptions {
			IntegrationKey = Config.IntegrationKey1,
			IsProduction = true,
			PrivateKey = Config.PrivateKey1,
			PublicKey = Config.PublicKey1,
			UserId = Config.UserId1
		});

		Assert.Equal(created, cached);
	}

	[Fact]
	public void CreateClients() {
		var client1 = _docuSignFactory.CreateClient(new DocuSignClientOptions {
			IntegrationKey = Config.IntegrationKey1,
			IsProduction = true,
			PrivateKey = Config.PrivateKey1,
			PublicKey = Config.PublicKey1,
			UserId = Config.UserId1
		});
		var client2 = _docuSignFactory.CreateClient(new DocuSignClientOptions {
			IntegrationKey = Config.IntegrationKey2,
			IsProduction = true,
			PrivateKey = Config.PrivateKey2,
			PublicKey = Config.PublicKey2,
			UserId = Config.UserId2
		});

		Assert.NotNull(client1);
		Assert.NotNull(client2);
	}
}