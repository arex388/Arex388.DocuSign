using NetEscapades.EnumGenerators;
using System.ComponentModel.DataAnnotations;

namespace Arex388.DocuSign;

/// <summary>
/// The <c>Envelope</c>'s status.
/// </summary>
[EnumExtensions]
public enum EnvelopeStatus :
	byte {
	/// <summary>
	/// The default status. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The envelope has been completed.
	/// </summary>
	[Display(Name = "completed")]
	Completed,

	/// <summary>
	/// The envelope has been created, but not sent.
	/// </summary>
	[Display(Name = "created")]
	Created,

	/// <summary>
	/// The envelope has been declined by the recipient(s).
	/// </summary>
	[Display(Name = "declined")]
	Declined,

	/// <summary>
	/// The envelope was delivered to the recipient(s).
	/// </summary>
	[Display(Name = "delivered")]
	Delivered,

	/// <summary>
	/// The envelope was sent to the recipient(s).
	/// </summary>
	[Display(Name = "sent")]
	Sent,

	/// <summary>
	/// The envelope was signed by the recipient(s).
	/// </summary>
	[Display(Name = "signed")]
	Signed,

	/// <summary>
	/// The envelope was voided by the sender.
	/// </summary>
	[Display(Name = "voided")]
	Voided
}