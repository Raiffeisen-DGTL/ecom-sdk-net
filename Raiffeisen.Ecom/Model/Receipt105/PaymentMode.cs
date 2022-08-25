using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Calculation method.
/// </summary>
[ComVisible(true)]
public enum PaymentMode
{
    /// <summary>
    ///     Advance payment before the transfer of the subject of calculation.
    /// </summary>
    [EnumMember(Value = "FULL_PREPAYMENT")]
    FullPrepayment,

    /// <summary>
    ///     Full payment at the time of transfer of the subject of calculation.
    /// </summary>
    [EnumMember(Value = "FULL_PAYMENT")] FullPayment
}