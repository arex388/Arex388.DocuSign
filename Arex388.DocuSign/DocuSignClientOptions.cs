namespace Arex388.DocuSign;

/// <summary>
/// DocuSignClientOptions object containing the data needed to authenticate with DocuSign.
/// </summary>
public sealed class DocuSignClientOptions {
	/// <summary>
	/// The DocuSign public encryption key.
	/// </summary>
	public string PublicKey { get; init; } = null!;

	/// <summary>
	/// The DocuSign private encryption key.
	/// </summary>
	public string PrivateKey { get; init; } = null!;

	/// <summary>
	/// The DocuSign integration key.
	/// </summary>
	public Guid IntegrationKey { get; init; }

	/// <summary>
	/// Flag indicating if the client will use development or production endpoints.
	/// </summary>
	public bool IsProduction { get; init; }

	/// <summary>
	/// The DocuSign user's id.
	/// </summary>
	public Guid UserId { get; init; }
}