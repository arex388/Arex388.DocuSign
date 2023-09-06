using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.DocuSign.Extensions;

/// <summary>
/// <c>IServiceCollection</c> extensions.
/// </summary>
public static class ServiceCollectionExtensions {
	/// <summary>
	/// Add DocuSign service for interacting with multiple accounts.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddDocuSign(
		this IServiceCollection services) => services.AddSingleton<IDocuSignClientFactory>(
		sp => {
			var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(DocuSignClientFactory));
			var memoryCache = sp.GetRequiredService<IMemoryCache>();

			return new DocuSignClientFactory(httpClient, memoryCache);
		});

	/// <summary>
	/// Add DocuSign service for interacting with a single account.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="options">The DocuSign keys.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddDocuSign(
		this IServiceCollection services,
		DocuSignClientOptions options) => services.AddSingleton<IDocuSignClient>(
		sp => {
			var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(DocuSignClient));
			var memoryCache = sp.GetRequiredService<IMemoryCache>();

			return new DocuSignClient(httpClient, memoryCache, options);
		});
}