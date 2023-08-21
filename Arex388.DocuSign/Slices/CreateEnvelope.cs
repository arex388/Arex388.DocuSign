using Arex388.DocuSign.Converters;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// CreateEnvelope request and response containers.
/// </summary>
public sealed class CreateEnvelope {
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
	/// CreateEnvelope request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// List of documents for the envelope.
		/// </summary>
		public IEnumerable<EnvelopeDocument> Documents { get; init; } = Enumerable.Empty<EnvelopeDocument>();

		/// <summary>
		/// The email's subject when sending the envelope.
		/// </summary>
		public string EmailSubject { get; init; } = null!;

		/// <summary>
		/// The envelope's recipients.
		/// </summary>
		public EnvelopeRecipients Recipients { get; init; } = null!;

		/// <summary>
		/// The envelope's status. Created by default.
		/// </summary>
		[JsonConverter(typeof(EnvelopeStatusJsonConverter))]
		public EnvelopeStatus Status { get; init; } = EnvelopeStatus.Created;
	}

	/// <summary>
	/// CreateEnvelope response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The envelope.
		/// </summary>
		public Envelope Envelope { get; init; } = null!;

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