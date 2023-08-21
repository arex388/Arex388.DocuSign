using Arex388.DocuSign.Converters;
using System.Text.Json.Serialization;

namespace Arex388.DocuSign;

/// <summary>
/// EnvelopeRecepientTab object.
/// </summary>
public sealed class EnvelopeRecipientTab {
	/// <summary>
	/// The string to search for in the document and anchor this tab to.
	/// </summary>
	public string? AnchorString { get; init; }

	/// <summary>
	/// The anchor's X offset adjustment.
	/// </summary>
	public int? AnchorXOffset { get; init; }

	/// <summary>
	/// The anchor's Y offset adjustment.
	/// </summary>
	public int? AnchorYOffset { get; init; }

	/// <summary>
	/// The anchor's offset unit.
	/// </summary>
	[JsonConverter(typeof(TabAnchorUnitJsonConverter))]
	public TabAnchorUnit AchorUnits { get; init; } = TabAnchorUnit.Pixels;

	/// <summary>
	/// Flag indicating if the tab is required or not.
	/// </summary>
	[JsonConverter(typeof(BooleanJsonConverter)), JsonPropertyName("optional")]
	public bool IsOptional { get; init; }

	/// <summary>
	/// The tab's name.
	/// </summary>
	public string Name { get; init; } = null!;

	/// <summary>
	/// The tab's recipient id in the context of the envelope.
	/// </summary>
	public int RecipientId { get; init; }

	/// <summary>
	/// The tab's scaling factor. Valid ranges are 0.5 to 2.0.
	/// </summary>
	[JsonPropertyName("scaleValue")]
	public decimal Scale { get; init; } = 1.0M;
}