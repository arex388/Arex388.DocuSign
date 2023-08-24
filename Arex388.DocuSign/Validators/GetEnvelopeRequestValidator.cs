using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class GetEnvelopeRequestValidator :
	AbstractValidator<GetEnvelope.Request> {
	public GetEnvelopeRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
	}
}