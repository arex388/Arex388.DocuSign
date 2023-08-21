using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

internal sealed class Authorization {
	[JsonPropertyName("access_token")]
	public string Token { get; init; } = null!;
}