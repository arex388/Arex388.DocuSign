using FluentValidation.Results;

namespace Arex388.DocuSign;

/// <summary>
/// VoidEnvelope request and response containers.
/// </summary>
public sealed class VoidEnvelope {
	internal static UpdateEnvelope.Response Invalid(
		ValidationResult validation) => new() {
			Errors = validation.GetErrors(),
			Status = ResponseStatus.Invalid
		};

	/// <summary>
	/// VoidEnvelope request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The envelope's id.
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// The reason for voiding.
		/// </summary>
		public string Reason { get; init; } = null!;
	}
}