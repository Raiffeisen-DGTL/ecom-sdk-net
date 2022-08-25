using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Transaction status.
/// </summary>
[ComVisible(true)]
public enum Value
{
    /// <summary>
    ///     New.
    /// </summary>
    [EnumMember(Value = "NEW")] New,

    /// <summary>
    ///     Cancelled.
    /// </summary>
    [EnumMember(Value = "CANCELLED")] Cancelled,

    /// <summary>
    ///     Expired.
    /// </summary>
    [EnumMember(Value = "EXPIRED")] Expired,

    /// <summary>
    ///     Paid.
    /// </summary>
    [EnumMember(Value = "PAID")] Paid
}