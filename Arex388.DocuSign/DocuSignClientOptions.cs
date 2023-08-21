namespace Arex388.DocuSign;

public sealed class DocuSignClientOptions {
	public string PublicKey { get; init; } = null!;
	public string PrivateKey { get; init; } = null!;
	public Guid IntegrationKey { get; init; }
	public bool IsProduction { get; init; }
	public Guid UserId { get; init; }
}