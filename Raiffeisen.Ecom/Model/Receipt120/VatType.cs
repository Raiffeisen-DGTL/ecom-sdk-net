using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     VAT rate per receipt item.
/// </summary>
[ComVisible(true)]
public enum VatType
{
    /// <summary>
    ///     Without VAT.
    /// </summary>
    [EnumMember(Value = "NONE")] None,

    /// <summary>
    ///     VAT at 0%.
    /// </summary>
    [EnumMember(Value = "VAT0")] Vat0,

    /// <summary>
    ///     VAT receipt at a rate of 10%.
    /// </summary>
    [EnumMember(Value = "VAT10")] Vat10,

    /// <summary>
    ///     VAT receipt at the estimated rate 10/110.
    /// </summary>
    [EnumMember(Value = "VAT110")] Vat110,

    /// <summary>
    ///     VAT receipt at a rate of 20%.
    /// </summary>
    [EnumMember(Value = "VAT20")] Vat20,

    /// <summary>
    ///     VAT receipt at the estimated rate of 20/120.
    /// </summary>
    [EnumMember(Value = "VAT120")] Vat120
}