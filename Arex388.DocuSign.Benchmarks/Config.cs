using Microsoft.Extensions.Configuration;

namespace Arex388.DocuSign.Benchmarks;

internal sealed class Config {
	private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder().AddUserSecrets<Config>().Build();

	private static Guid? _integrationKey;
	private static Guid? _userId;

	public static string DocuSignFilePath => _configuration[nameof(DocuSignFilePath)]!;
	public static Guid IntegrationKey => _integrationKey ??= Guid.Parse(_configuration[nameof(IntegrationKey)]!);
	public static string PublicKey => _configuration[nameof(PublicKey)]!;
	public static string PrivateKey => _configuration[nameof(PrivateKey)]!;
	public static string RecipientEmail => _configuration[nameof(RecipientEmail)]!;
	public static string RecipientName => _configuration[nameof(RecipientName)]!;
	public static Guid UserId => _userId ??= Guid.Parse(_configuration[nameof(UserId)]!);
}