using Xunit;

namespace Arex388.DocuSign.Tests;

public sealed class Envelope {
	private readonly IDocuSignClient _docuSign = new DocuSignClient(new HttpClient(), new DocuSignClientOptions {
		IntegrationKey = Config.IntegrationKey1,
		PrivateKey = Config.PrivateKey1,
		PublicKey = Config.PublicKey1,
		UserId = Config.UserId1
	});

	[Fact]
	public async Task CreateAndGetAndVoidAsync() {
		var pdfBytes = await File.ReadAllBytesAsync(Config.DocuSignFilePath).ConfigureAwait(false);
		var create = await _docuSign.CreateEnvelopeAsync(new CreateEnvelope.Request {
			Documents = new[] {
				new EnvelopeDocument {
					Base64 = Convert.ToBase64String(pdfBytes),
					Display = EnvelopeDocumentDisplay.Inline,
					Extension = "pdf",
					Id = 1,
					Name = "DocuSign"
				}
			},
			EmailSubject = "TEST",
			Recipients = new EnvelopeRecipients {
				Signers = new[] {
					new EnvelopeRecipient{
						Email = Config.RecipientEmail,
						Id = 1,
						Name = Config.RecipientName,
						Tabs = new EnvelopeRecipientTabs {
							SignHereTabs = new[] {
								new EnvelopeRecipientTab {
									AnchorString = "Signature of Signee",
									AnchorXOffset = 5,
									AnchorYOffset = 36,
									AchorUnits = TabAnchorUnit.Millimeter,
									Name = "Signature of Signee",
									RecipientId = 1,
									Scale = 1.51M
								}
							}
						}
					}
				}
			},
			Status = EnvelopeStatus.Sent
		}).ConfigureAwait(false);

		Assert.Equal(ResponseStatus.Succeeded, create.Status);
		Assert.NotNull(create.Envelope);

		var get = await _docuSign.GetEnvelopeAsync(new GetEnvelope.Request {
			Id = create.Envelope.Id
		}).ConfigureAwait(false);

		Assert.Equal(ResponseStatus.Succeeded, get.Status);

		var @void = await _docuSign.VoidEnvelopeAsync(new VoidEnvelope.Request {
			Id = create.Envelope.Id,
			Reason = "Not needed"
		}).ConfigureAwait(false);

		Assert.Equal(ResponseStatus.Succeeded, @void.Status);
	}
}