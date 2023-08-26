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