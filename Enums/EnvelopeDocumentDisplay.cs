using NetEscapades.EnumGenerators;
using System.ComponentModel.DataAnnotations;

namespace Arex388.DocuSign;

/// <summary>
/// The <c>EnvelopeDocument</c>'s display.
/// </summary>
[EnumExtensions]
public enum EnvelopeDocumentDisplay :
	byte {
	/// <summary>
	/// The default display. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The document's display mode is inline (default).
	/// </summary>
	[Display(Name = "inline")]
	Inline,

	/// <summary>
	/// The document's display mode is in a modal.
	/// </summary>
	[Display(Name = "modal")]
	Modal
}