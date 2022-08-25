using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Payment method.
/// </summary>
[ComVisible(true)]
public enum PaymentMethod
{
    /// <summary>
    ///     Only Fast Payment System.
    /// </summary>
    [EnumMember(Value = "ONLY_SBP")] Sbp,

    /// <summary>
    ///     Only acquiring.
    /// </summary>
    [EnumMember(Value = "ONLY_ACQUIRING")] Acquiring
}