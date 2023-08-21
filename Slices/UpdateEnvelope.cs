using Arex388.DocuSign.Converters;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// UpdateEnvelope request and response containers.
/// </summary>
public sealed class UpdateEnvelope {
	private static Response? _cancelled;
	private static Response? _failed;
	private static Response? _timedOut;

	internal static Response Cancelled => _cancelled ??= new Response {
		Status = ResponseStatus.Cancelled
	};
	internal static Response Failed(
		Exception? exception) => exception is null
		? _failed ??= new Response {
			Status = ResponseStatus.Failed
		}
		: Failed($"[{exception.GetType().Name}] {exception.Message}");
	internal static Response Failed(
		string error) => new() {
			Errors = new[] {
				error
			},
			Status = ResponseStatus.Failed
		};
	internal static Response Invalid(
		ValidationResult validation) => new() {
			Errors = validation.GetErrors(),
			Status = ResponseStatus.Invalid
		};
	internal static Response TimedOut => _timedOut ??= new Response {
		Status = ResponseStatus.TimeOut
	};

	/// <summary>
	/// UpdateEnvelope request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The envelope's id.
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// The envelope's status.
		/// </summary>
		[JsonConverter(typeof(EnvelopeStatusJsonConverter))]
		public EnvelopeStatus? Status { get; init; }

		/// <summary>
		/// The reason for voiding the envelope if status is Voided.
		/// </summary>
		public string? VoidedReason { get; init; }
	}

	/// <summary>
	/// UpdateEnvelope response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's errors, if any.
		/// </summary>
		public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }
	}
}