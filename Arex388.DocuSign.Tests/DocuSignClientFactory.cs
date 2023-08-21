using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace Arex388.DocuSign.Tests;

public sealed class DocuSignClientFactory {
	private readonly IDocuSignClientFactory _docuSignFactory = new DocuSign.DocuSignClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));

	[Fact]
	public void CreateAndCacheClient() {
		var created = _docuSignFactory.CreateClient(Config.IntegrationKey1, Config.UserId1, Config.PublicKey1, Config.PrivateKey1);
		var cached = _docuSignFactory.CreateClient(Config.IntegrationKey1, Config.UserId1, Config.PublicKey1, Config.PrivateKey1);

		Assert.Equal(created, cached);
	}

	[Fact]
	public void CreateClients() {
		var client1 = _docuSignFactory.CreateClient(Config.IntegrationKey1, Config.UserId1, Config.PublicKey1, Config.PrivateKey1);
		var client2 = _docuSignFactory.CreateClient(Config.IntegrationKey2, Config.UserId2, Config.PublicKey2, Config.PrivateKey2);

		Assert.NotNull(client1);
		Assert.NotNull(client2);
	}
}