using FluentValidation;

namespace Arex388.DocuSign.Validators;

internal sealed class EnvelopeRecipientTabsValidator :
	AbstractValidator<EnvelopeRecipientTabs> {
	public EnvelopeRecipientTabsValidator() {
		RuleFor(r => r.SignHereTabs).NotEmpty();
	}
}