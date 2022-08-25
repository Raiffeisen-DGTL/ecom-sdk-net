using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Payment type.
/// </summary>
[ComVisible(true)]
public enum Type
{
    /// <summary>
    ///     Cashless payment.
    /// </summary>
    [EnumMember(Value = "E_PAYMENT")] EPayment,

    /// <summary>
    ///     Advance payment (offset of advance payment and/or previous payments).
    /// </summary>
    [EnumMember(Value = "PREPAID")] Prepaid
}