using Microsoft.Extensions.Configuration;

namespace Arex388.DocuSign.Tests;

internal sealed class Config {
	private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder().AddUserSecrets<Config>().Build();

	private static Guid? _integrationKey1;
	private static Guid? _integrationKey2;
	private static Guid? _userId1;
	private static Guid? _userId2;

	public static string DocuSignFilePath => _configuration[nameof(DocuSignFilePath)]!;
	public static Guid IntegrationKey1 => _integrationKey1 ??= Guid.Parse(_configuration["IntegrationKey-1"]!);
	public static Guid IntegrationKey2 => _integrationKey2 ??= Guid.Parse(_configuration["IntegrationKey-2"]!);
	public static string PublicKey1 => _configuration["PublicKey-1"]!;
	public static string PublicKey2 => _configuration["PublicKey-2"]!;
	public static string PrivateKey1 => _configuration["PrivateKey-1"]!;
	public static string PrivateKey2 => _configuration["PrivateKey-2"]!;
	public static string RecipientEmail => _configuration[nameof(RecipientEmail)]!;
	public static string RecipientName => _configuration[nameof(RecipientName)]!;
	public static Guid UserId1 => _userId1 ??= Guid.Parse(_configuration["UserId-1"]!);
	public static Guid UserId2 => _userId2 ??= Guid.Parse(_configuration["UserId-2"]!);
}