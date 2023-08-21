using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class EnvelopeRecipientTabValidator :
	AbstractValidator<EnvelopeRecipientTab> {
	public EnvelopeRecipientTabValidator() {
		RuleFor(r => r.Name).NotEmpty();
		RuleFor(r => r.RecipientId).NotEmpty();
		RuleFor(r => r.Scale).InclusiveBetween(0.5M, 2.0M);
	}
}