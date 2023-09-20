using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class CreateEnvelopeRequestValidator :
	AbstractValidator<CreateEnvelope.Request> {
	public CreateEnvelopeRequestValidator() {
		RuleFor(r => r.Documents).NotEmpty();
		RuleFor(r => r.EmailSubject).MaximumLength(100).NotEmpty();
		RuleFor(r => r.Recipients).NotEmpty();
		RuleFor(r => r.Status).NotEmpty().NotEqual(EnvelopeStatus.None);
	}
}