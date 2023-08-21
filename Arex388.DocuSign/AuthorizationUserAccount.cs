using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

internal sealed class AuthorizationUserAccount {
	[JsonPropertyName("base_uri")]
	public string BaseUrl { get; init; } = null!;

	[JsonPropertyName("account_id")]
	public Guid Id { get; init; }

	[JsonPropertyName("is_default")]
	public bool IsDefault { get; init; }
}