# Arex388.DocuSign

Arex388.DocuSign is a highly opinionated .NET Standard 2.0 library for the DocuSign API. It is intended to be an easy, well structured, and highly performant client for interacting with DocuSign's API for eSignature management. It can be used in applications interacting with a single account using `IDocuSignClient`, or with applications interacting with multiple accounts using `IDocuSignClientFactory`.

As noted above, it is highly opinionated. The API documentation is honestly not great or very explanatory. I ended up just doing trial-and-error test calls until the API stopped responding with errors.

> **NOTE**
>
> The current implementation does not cover the full range of the API's functionality. I only needed a small subset right now, so that's all it does. In the future the foundation is there for me to slowly expand upon it, or you can if you want to fork it.

The library has built-in dependency injection to simplify usage. By default a singleton `IDocuSignClient` or `IDocuSignClientFactory` instance will be created. If using `IDocuSignClientFactory`, it will cache `IDocuSignClient` instances by their API key.

- [Changelog](CHANGELOG.md)
- [Benchmarks](BENCHMARKS.md)



#### Dependency Injection

To configure dependency injection with ASP.NET and ASP.NET Core, use `AddDocuSign()` extensions on `IServiceCollection`. There are two signatures, with and without `options` parameter. If `options` is passed to the extension, it will register `IDocuSignClient` for use with a single account, otherwise it will register `IDocuSignClientFactory` for use with multiple accounts.



#### How to Use

You can create an envelope, update it, and void it using:

```c#
//	Create Envelope
await docuSign.CreateEnvelopeAsync(new CreateEnvelope.Request {
    ...
});

//	Update Envelope
await docuSign.UpdateEnvelopeAsync(new UpdateEnvelope.Request {
	...
});

//	Void Envelope
await docuSign.VoidEnvelopeAsync(new VoidEnvelope.Request {
    ...
});
```

