#### 1.0.7 (2023-09-05)

- Removed authorization tracking all together. Each call will now refresh the authorization token, which will come with a slight performance impact. Since DocuSign wants to make a ridiculously complicated authorization process under the pretense of "security", I'll just skip it and force their API to generate new tokens on each request.

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

- Initial implementation.