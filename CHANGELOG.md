#### 2.0.3 (2023-10-21)

- **Changed:** How authorization header is changed on the `HttpClient` because the current implementation was throwing exceptions.

#### 2.0.2 (2023-09-27)

- **Updated:** Package dependencies.

#### 2.0.1 (2023-09-15)

- **Added:** A validation rule for the envelope email subject length to be less than or equal to 100 characters.

#### 2.0.0 (2023-09-06)

- **Added:** A new implementation for authorization tracking and caching using `IMemoryCache` since it was already a dependency for the library, and was being used in `IDocuSignClientFactory`. Hopefully, authorization regeneration and tracking is more robust now and reduces unnecessary resource usage. It is a breaking change because the `IDocuSign` constructor changed to accept an instance of `IMemoryCache`, thus the major version bump.

#### 1.0.7 (2023-09-05)

- **Removed:** Authorization tracking all together. Each call will now refresh the authorization token, which will come with a slight performance impact. Since DocuSign wants to make a ridiculously complicated authorization process under the pretense of "security", I'll just skip it and force their API to generate new tokens on each request.

#### 1.0.6 (2023-09-05)

- Bug fix for authorization tracking.

#### 1.0.5 (2023-08-31)

- **Changed:** Removed the internal auto-regeneration for the JWT on a 45-minute delay. Instead when API calls are made, the call will wait for the JWT to be refreshed and authorization to be completed first.

#### 1.0.4 (2023-08-25)

- **Added:** The ability to get the user authorization URL with `GetUserAuthorizationUrl()`.

#### 1.0.3 (2023-08-24)

- **Added:** The ability to query (limited) an envelope with `GetEnvelopeAsync()`.
- Minor internal changes.

#### 1.0.2 (2023-08-23)

- Added the ability to set an expiration on an envelope.

#### 1.0.1 (2023-08-22)

- Minor cleanup and fixes.
- **Breaking Change:** Changed `IDocuSignFactory.CreateClient()` signature to only accept an instance of `DocuSignClientOptions` and forward it to the client instance.

#### 1.0.0 (2023-08-21)

- Initial release.