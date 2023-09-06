namespace Arex388.DocuSign;

internal sealed class AuthorizationUser {
	private AuthorizationUserAccount? _defaultAccount;

	public IEnumerable<AuthorizationUserAccount> Accounts { get; init; } = Enumerable.Empty<AuthorizationUserAccount>();

	public AuthorizationUserAccount DefaultAccount => _defaultAccount ??= Accounts.First(
		a => a.IsDefault);
}