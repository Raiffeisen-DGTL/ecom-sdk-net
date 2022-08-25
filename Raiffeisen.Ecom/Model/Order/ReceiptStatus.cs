using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Receipt registration status.
/// </summary>
[ComVisible(true)]
public enum ReceiptStatus
{
    /// <summary>
    ///     Receipt in rocess of registration.
    /// </summary>
    [EnumMember(Value = "NEW")] New,

    /// <summary>
    ///     Receipt in process of registration.
    /// </summary>
    [EnumMember(Value = "IN_PROGRESS")] InProgress,

    /// <summary>
    ///     Receipt registered successfully.
    /// </summary>
    [EnumMember(Value = "DONE")] Done,

    /// <summary>
    ///     Receipt registration ended with an error.
    /// </summary>
    [EnumMember(Value = "FAILED")] Failed,

    /// <summary>
    ///     Technical status before sending a receipt for registration.
    /// </summary>
    [EnumMember(Value = "AWAITING")] Awaiting
}