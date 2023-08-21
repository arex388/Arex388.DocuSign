using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class EnvelopeRecipientValidator :
	AbstractValidator<EnvelopeRecipient> {
	public EnvelopeRecipientValidator() {
		RuleFor(r => r.Email).EmailAddress().NotEmpty();
		RuleFor(r => r.Id).NotEmpty();
		RuleFor(r => r.Name).NotEmpty();
		RuleFor(r => r.Tabs).NotEmpty();
	}
}