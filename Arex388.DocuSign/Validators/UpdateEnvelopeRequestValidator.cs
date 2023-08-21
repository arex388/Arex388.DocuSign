using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class UpdateEnvelopeRequestValidator :
	AbstractValidator<UpdateEnvelope.Request> {
	public UpdateEnvelopeRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
		RuleFor(r => r.VoidedReason).NotEmpty().When(r => r.Status == EnvelopeStatus.Voided);
	}
}