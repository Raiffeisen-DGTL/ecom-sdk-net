using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Payment type.
/// </summary>
[ComVisible(true)]
public enum PaymentMethod
{
    /// <summary>
    ///     Payment via Fast Payment System.
    /// </summary>
    [EnumMember(Value = "sbp")] Sbp,

    /// <summary>
    ///     Card payment.
    /// </summary>
    [EnumMember(Value = "acquiring")] Acquiring
}