namespace Arex388.DocuSign;

internal sealed class AuthorizationUser {
	public IEnumerable<AuthorizationUserAccount> Accounts { get; init; } = Enumerable.Empty<AuthorizationUserAccount>();

	public AuthorizationUserAccount DefaultAccount => Accounts.First(
		a => a.IsDefault);
}