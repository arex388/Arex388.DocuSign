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
		@if => {
			var httpClientFactory = @if.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(DocuSignClientFactory));
			var memoryCache = @if.GetRequiredService<IMemoryCache>();

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
		@if => {
			var httpClientFactory = @if.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(DocuSignClient));

			return new DocuSignClient(httpClient, options);
		});
}