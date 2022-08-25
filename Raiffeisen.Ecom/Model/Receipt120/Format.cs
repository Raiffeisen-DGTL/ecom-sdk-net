using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Marking code format.
/// </summary>
[ComVisible(true)]
public enum Format
{
    /// <summary>
    ///     Unknown.
    /// </summary>
    [EnumMember(Value = "UNKNOWN")] Unknown,

    /// <summary>
    ///     EAN8.
    /// </summary>
    [EnumMember(Value = "EAN8")] Ean8,

    /// <summary>
    ///     EAN13.
    /// </summary>
    [EnumMember(Value = "EAN13")] Ean13,

    /// <summary>
    ///     ITF14.
    /// </summary>
    [EnumMember(Value = "ITF14")] Itf14,

    /// <summary>
    ///     GS1M.
    /// </summary>
    [EnumMember(Value = "GS1M")] Gs1M,

    /// <summary>
    ///     Short.
    /// </summary>
    [EnumMember(Value = "SHORT")] Short,

    /// <summary>
    ///     FUR.
    /// </summary>
    [EnumMember(Value = "FUR")] Fur,

    /// <summary>
    ///     EGAIS20.
    /// </summary>
    [EnumMember(Value = "EGAIS20")] Egais20,

    /// <summary>
    ///     EGAIS30.
    /// </summary>
    [EnumMember(Value = "EGAIS30")] Egais30
}