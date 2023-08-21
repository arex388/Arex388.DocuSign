using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class VoidEnvelopeRequestValidator :
	AbstractValidator<VoidEnvelope.Request> {
	public VoidEnvelopeRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
		RuleFor(r => r.Reason).NotEmpty();
	}
}