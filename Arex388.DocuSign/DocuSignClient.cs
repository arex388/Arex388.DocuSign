﻿using Arex388.DocuSign.Extensions;
using Arex388.DocuSign.Validators;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable MethodHasAsyncOverloadWithCancellation

namespace Arex388.DocuSign;

/// <summary>
/// DocuSign API client.
/// </summary>
public sealed class DocuSignClient :
	IDocuSignClient {
	private static readonly JsonSerializerOptions _jsonSerializerOptions = new() {
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};
	private static readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new() {
		AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(45),
		Priority = CacheItemPriority.NeverRemove
	};

	private readonly HttpClient _httpClient;
	private readonly IMemoryCache _memoryCache;
	private readonly DocuSignClientOptions _options;

	private CreateEnvelopeRequestValidator? _createEnvelopeRequestValidator;
	private EnvelopeRecipientsValidator? _envelopeRecipientsValidator;
	private EnvelopeRecipientValidator? _envelopeRecipientValidator;
	private EnvelopeRecipientTabsValidator? _envelopeRecipientTabsValidator;
	private EnvelopeRecipientTabValidator? _envelopeRecipientTabValidator;
	private UpdateEnvelopeRequestValidator? _updateEnvelopeRequestValidator;
	private AuthorizationUrls? _urls;
	private VoidEnvelopeRequestValidator? _voidEnvelopeRequestValidator;

	/// <summary>
	/// Create an instance of the DocuSign API client.
	/// </summary>
	/// <param name="httpClient">An instance of <c>HttpClient</c>.</param>
	/// <param name="memoryCache">An instance of <c>MemoryCache</c>.</param>
	/// <param name="options">An instance of <c>DocuSignClientOptions</c>.</param>
	public DocuSignClient(
		HttpClient httpClient,
		IMemoryCache memoryCache,
		DocuSignClientOptions options) {
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		_options = options ?? throw new ArgumentNullException(nameof(options));
	}

	//	============================================================================
	//	Actions
	//	============================================================================

	/// <summary>
	/// Create an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>CreateEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>CreateEnvelope.Response</c>.</returns>
	public async Task<CreateEnvelope.Response> CreateEnvelopeAsync(
		CreateEnvelope.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return CreateEnvelope.Cancelled;
		}

		var account = await GetAccountAsync().ConfigureAwait(false);

		if (account is null) {
			return CreateEnvelope.Failed("The authorization failed");
		}

		var createEnvelopeValidator = _createEnvelopeRequestValidator ??= new CreateEnvelopeRequestValidator();
		var createEnvelopeValidation = createEnvelopeValidator.Validate(request);

		if (!createEnvelopeValidation.IsValid) {
			return CreateEnvelope.Invalid(createEnvelopeValidation);
		}

		var envelopeRecipientsValidator = _envelopeRecipientsValidator ??= new EnvelopeRecipientsValidator();
		var envelopeRecipientsValidation = envelopeRecipientsValidator.Validate(request.Recipients);

		if (!envelopeRecipientsValidation.IsValid) {
			return CreateEnvelope.Invalid(envelopeRecipientsValidation);
		}

		var envelopeRecipientValidator = _envelopeRecipientValidator ??= new EnvelopeRecipientValidator();

		foreach (var signer in request.Recipients.Signers) {
			var envelopeRecipientValidation = envelopeRecipientValidator.Validate(signer);

			if (envelopeRecipientValidation.IsValid) {
				continue;
			}

			return CreateEnvelope.Invalid(envelopeRecipientValidation);
		}

		var envelopeRecipientTabsValidator = _envelopeRecipientTabsValidator ??= new EnvelopeRecipientTabsValidator();

		foreach (var tabs in request.Recipients.Signers.Select(s => s.Tabs)) {
			var envelopeRecipientTabsValidation = envelopeRecipientTabsValidator.Validate(tabs);

			if (envelopeRecipientTabsValidation.IsValid) {
				continue;
			}

			return CreateEnvelope.Invalid(envelopeRecipientTabsValidation);
		}

		var envelopeRecipientTabValidator = _envelopeRecipientTabValidator ??= new EnvelopeRecipientTabValidator();

		foreach (var tab in request.Recipients.Signers.Select(s => s.Tabs).SelectMany(t => t.SignHereTabs)) {
			var envelopeRecipientTabValidation = envelopeRecipientTabValidator.Validate(tab);

			if (envelopeRecipientTabValidation.IsValid) {
				continue;
			}

			return CreateEnvelope.Invalid(envelopeRecipientTabValidation);
		}

		if (cancellationToken.IsCancellationRequested) {
			return CreateEnvelope.Cancelled;
		}

		try {
			var response = await _httpClient.PostAsJsonAsync(request.GetEndpoint(account), request, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

			if (response.IsSuccessStatusCode) {
				var envelope = await response.Content.ReadFromJsonAsync<Envelope>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

				return new CreateEnvelope.Response {
					Envelope = envelope!,
					Status = ResponseStatus.Succeeded
				};
			}

			var error = await response.Content.ReadFromJsonAsync<FailedResponse>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

			return CreateEnvelope.Failed(error!.ToString());
		} catch (TaskCanceledException) {
			return CreateEnvelope.TimedOut;
		} catch (Exception e) {
			return CreateEnvelope.Failed(e);
		}
	}

	/// <summary>
	/// Get an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>GetEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetEnvelope.Response</c>.</returns>
	public async Task<GetEnvelope.Response> GetEnvelopeAsync(
		GetEnvelope.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return GetEnvelope.Cancelled;
		}

		var account = await GetAccountAsync().ConfigureAwait(false);

		if (account is null) {
			return GetEnvelope.Failed("The authorization failed");
		}

		var validator = new GetEnvelopeRequestValidator();
		var validation = validator.Validate(request);

		if (!validation.IsValid) {
			return GetEnvelope.Invalid(validation);
		}

		if (cancellationToken.IsCancellationRequested) {
			return GetEnvelope.Cancelled;
		}

		try {
			var response = await _httpClient.GetAsync(request.GetEndpoint(account), cancellationToken).ConfigureAwait(false);

			if (response.IsSuccessStatusCode) {
				var envelope = await response.Content.ReadFromJsonAsync<Envelope>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

				return new GetEnvelope.Response {
					Envelope = envelope!,
					Status = ResponseStatus.Succeeded
				};
			}

			var error = await response.Content.ReadFromJsonAsync<FailedResponse>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

			return GetEnvelope.Failed(error!.ToString());
		} catch (TaskCanceledException) {
			return GetEnvelope.TimedOut;
		} catch (Exception e) {
			return GetEnvelope.Failed(e);
		}
	}

	/// <summary>
	/// Get the user authorization URL.
	/// </summary>
	/// <param name="redirectUrl">The URL to redirect to after the user is authorized. The URL must be identical to one or more enabled redirect URLs in DocuSign.</param>
	/// <returns>The URL string.</returns>
	public string GetUserAuthorizationUrl(
		string redirectUrl) => _options.IsProduction
		? $"https://account.docusign.com/oauth/auth?response_type=code&scope=signature%20impersonation&client_id={_options.IntegrationKey}&redirect_uri={redirectUrl}"
		: $"https://account-d.docusign.com/oauth/auth?response_type=code&scope=signature%20impersonation&client_id={_options.IntegrationKey}&redirect_uri={redirectUrl}";

	/// <summary>
	/// Update an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>UpdateEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	public async Task<UpdateEnvelope.Response> UpdateEnvelopeAsync(
		UpdateEnvelope.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return UpdateEnvelope.Cancelled;
		}

		var account = await GetAccountAsync().ConfigureAwait(false);

		if (account is null) {
			return UpdateEnvelope.Failed("The authorization failed");
		}

		var validator = _updateEnvelopeRequestValidator ??= new UpdateEnvelopeRequestValidator();
		var validation = validator.Validate(request);

		if (!validation.IsValid) {
			return UpdateEnvelope.Invalid(validation);
		}

		if (cancellationToken.IsCancellationRequested) {
			return UpdateEnvelope.Cancelled;
		}

		try {
			var response = await _httpClient.PutAsJsonAsync(request.GetEndpoint(account), request, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

			if (response.IsSuccessStatusCode) {
				return new UpdateEnvelope.Response {
					Status = ResponseStatus.Succeeded
				};
			}

			var error = await response.Content.ReadFromJsonAsync<FailedResponse>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

			return UpdateEnvelope.Failed(error!.ToString());
		} catch (TaskCanceledException) {
			return UpdateEnvelope.TimedOut;
		} catch (Exception e) {
			return UpdateEnvelope.Failed(e);
		}
	}

	/// <summary>
	/// Void an envelope.
	/// </summary>
	/// <param name="request">An instance of <c>VoidEnvelope.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>UpdateEnvelope.Response</c>.</returns>
	public Task<UpdateEnvelope.Response> VoidEnvelopeAsync(
		VoidEnvelope.Request request,
		CancellationToken cancellationToken = default) {
		var validator = _voidEnvelopeRequestValidator ??= new VoidEnvelopeRequestValidator();
		var validation = validator.Validate(request);

		return !validation.IsValid
			? Task.FromResult(VoidEnvelope.Invalid(validation))
			: UpdateEnvelopeAsync(request.ToUpdateEnvelopeRequest(), cancellationToken);
	}

	///// <summary>
	///// Returns a user.
	///// </summary>
	///// <param name="cancellationToken">The cancellation token.</param>
	///// <returns>An instance of <c>GetUser.Response</c>.</returns>
	//public async Task<GetUser.Response> GetUserAsync(
	//	CancellationToken cancellationToken = default) {
	//	if (cancellationToken.IsCancellationRequested) {
	//		return GetUser.Cancelled;
	//	}

	//	await WaitForAuthorizationAsync().ConfigureAwait(false);

	//	try {
	//		var user = await _httpClient.GetFromJsonAsync<User?>($"{Account!.BaseUrl}/restapi/v2.1/accounts/{Account!.Id}/users/{_options.UserId}", cancellationToken).ConfigureAwait(false);

	//		return new GetUser.Response {
	//			Status = ResponseStatus.Succeeded,
	//			User = user
	//		};
	//	} catch (TaskCanceledException) {
	//		return GetUser.TimedOut;
	//	} catch (Exception e) {
	//		return GetUser.Failed(e);
	//	}
	//}

	//	============================================================================
	//	Utilities
	//	============================================================================

	/// <summary>
	/// Returns the authorization account.
	/// </summary>
	private async Task<AuthorizationUserAccount?> GetAccountAsync() {
		var key = $"{nameof(Arex388)}.{nameof(DocuSign)}.Key[{_options.IntegrationKey}].Account";

		if (_memoryCache.TryGetValue(key, out AuthorizationUserAccount? account)
			&& account is not null) {
			return account;
		}

		var urls = GetUrls();
		var jwt = GetJwt(urls);
		var form = new FormUrlEncodedContent(new[] {
			new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"),
			new KeyValuePair<string, string>("assertion", jwt)
		});

		var authorizationResponse = await _httpClient.PostAsync(urls.AuthorizationToken, form).ConfigureAwait(false);

		if (!authorizationResponse.IsSuccessStatusCode) {
			return null;
		}

		var authorization = await authorizationResponse.Content.ReadFromJsonAsync<Authorization>().ConfigureAwait(false);

		if (authorization is null) {
			return null;
		}

		//_httpClient.DefaultRequestHeaders.Remove("Authorization");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization.Token);
		//_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization.Token}");

		var userResponse = await _httpClient.GetFromJsonAsync<AuthorizationUser>(urls.AuthorizationUser).ConfigureAwait(false);

		if (userResponse is null) {
			return null;
		}

		_memoryCache.Set(key, userResponse.DefaultAccount, _memoryCacheEntryOptions);

		return userResponse.DefaultAccount;
	}

	/// <summary>
	/// Returns the generated JWT.
	/// </summary>
	/// <returns>The JWT.</returns>
	private string GetJwt(
		AuthorizationUrls urls) {
		var epoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var privateRsa = GetRsa(_options.PrivateKey, true);
		var publicRsa = GetRsa(_options.PublicKey);

		return JwtBuilder.Create()
						 .WithAlgorithm(new RS256Algorithm(publicRsa, privateRsa))
						 .AddClaim("iss", _options.IntegrationKey)
						 .AddClaim("sub", _options.UserId)
						 .AddClaim("aud", urls.Authorization)
						 .AddClaim("iat", epoch)
						 .AddClaim("exp", epoch + 6000)
						 .AddClaim("scope", "signature impersonation")
						 .Encode();
	}

	/// <summary>
	/// Returns an instance of <c>RSA</c> from a PEM string.
	/// </summary>
	/// <param name="pem">The PEM string.</param>
	/// <param name="isPrivate">Flag indicating if the PEM is for the Public or Private key.</param>
	/// <returns>An instance of <c>RSA</c>.</returns>
	private static RSA GetRsa(
		string pem,
		bool isPrivate = false) {
		using var reader = new StringReader(pem);
		using var pemer = new PemReader(reader);

		var key = pemer.ReadObject();
		var parameters = isPrivate
			? DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)key).Private)
			: DotNetUtilities.ToRSAParameters((RsaKeyParameters)key);

		var rsa = RSA.Create();

		rsa.ImportParameters(parameters);

		return rsa;
	}

	/// <summary>
	/// Returns the URLs for authorization generation.
	/// </summary>
	private AuthorizationUrls GetUrls() => _urls ??= _options.IsProduction
		? new AuthorizationUrls {
			Authorization = "account.docusign.com",
			AuthorizationToken = "https://account.docusign.com/oauth/token",
			AuthorizationUser = "https://account.docusign.com/oauth/userinfo"
		}
		: new AuthorizationUrls {
			Authorization = "account-d.docusign.com",
			AuthorizationToken = "https://account-d.docusign.com/oauth/token",
			AuthorizationUser = "https://account-d.docusign.com/oauth/userinfo"
		};
}