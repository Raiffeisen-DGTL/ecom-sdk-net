using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Receipt type.
/// </summary>
[ComVisible(true)]
public enum ReceiptType
{
    /// <summary>
    ///     Income receipt.
    /// </summary>
    [EnumMember(Value = "SELL")] Sell,

    /// <summary>
    ///     Refund receipt.
    /// </summary>
    [EnumMember(Value = "REFUND")] Refund
}