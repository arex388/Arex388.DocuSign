using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class EnvelopeRecipientsValidator :
	AbstractValidator<EnvelopeRecipients> {
	public EnvelopeRecipientsValidator() {
		RuleFor(r => r.Signers).NotEmpty();
	}
}