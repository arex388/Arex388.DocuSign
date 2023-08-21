using BenchmarkDotNet.Attributes;

namespace Arex388.DocuSign.Benchmarks;

[MaxIterationCount(16), MemoryDiagnoser]
public class Envelope {
	private readonly IDocuSignClient _docuSign = new DocuSignClient(new HttpClient(), new DocuSignClientOptions {
		IntegrationKey = Config.IntegrationKey,
		PrivateKey = Config.PrivateKey,
		PublicKey = Config.PublicKey,
		UserId = Config.UserId
	});
	private readonly byte[] _pdfBytes = File.ReadAllBytes(Config.DocuSignFilePath);

	[Benchmark]
	public async Task CreateAsync() {
		var response = await _docuSign.CreateEnvelopeAsync(new CreateEnvelope.Request {
			Documents = new[] {
				new EnvelopeDocument {
					Base64 = Convert.ToBase64String(_pdfBytes),
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

		_ = await _docuSign.VoidEnvelopeAsync(new VoidEnvelope.Request {
			Id = response.Envelope.Id
		}).ConfigureAwait(false);
	}
}