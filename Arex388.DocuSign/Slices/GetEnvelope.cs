using FluentValidation.Results;

namespace Arex388.DocuSign;

/// <summary>
/// GetEnvelope request and response containers.
/// </summary>
public sealed class GetEnvelope {
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
	/// GetEnvelope request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The envelope's id.
		/// </summary>
		public Guid Id { get; init; }
	}

	/// <summary>
	/// GetEnvelope response container.
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