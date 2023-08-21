using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

internal sealed class FailedResponse {
	[JsonPropertyName("errorCode")]
	public string Code { get; init; } = null!;

	public string Message { get; init; } = null!;
}