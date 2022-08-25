using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Transaction status.
/// </summary>
[ComVisible(true)]
public enum Value
{
    /// <summary>
    ///     Product.
    /// </summary>
    [EnumMember(Value = "SUCCESS")] Success,

    /// <summary>
    ///     Product.
    /// </summary>
    [EnumMember(Value = "NOT_FOUND")] NotFound
}