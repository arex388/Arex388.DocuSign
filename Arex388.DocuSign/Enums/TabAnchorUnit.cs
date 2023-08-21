using NetEscapades.EnumGenerators;
using System.ComponentModel.DataAnnotations;

namespace Arex388.DocuSign;

/// <summary>
/// The achor's offset unit.
/// </summary>
[EnumExtensions]
public enum TabAnchorUnit :
	byte {
	/// <summary>
	/// The default unit. If this is the value, then the response wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The anchor offset unit is in centimeters.
	/// </summary>
	[Display(Name = "cms")]
	Centimeter,

	/// <summary>
	/// The anchor offset unit is in inches.
	/// </summary>
	[Display(Name = "inches")]
	Inches,

	/// <summary>
	/// The anchor offset unit is in millimeters.
	/// </summary>
	[Display(Name = "mms")]
	Millimeter,

	/// <summary>
	/// The anchor offset unit is in pixels.
	/// </summary>
	[Display(Name = "pixels")]
	Pixels
}