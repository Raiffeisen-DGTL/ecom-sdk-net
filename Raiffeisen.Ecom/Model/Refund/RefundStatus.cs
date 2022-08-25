using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Request status code.
/// </summary>
[ComVisible(true)]
public enum RefundStatus
{
    /// <summary>
    ///     In progress.
    /// </summary>
    [EnumMember(Value = "IN_PROGRESS")] InProgress,

    /// <summary>
    ///     Completed.
    /// </summary>
    [EnumMember(Value = "COMPLETED")] Completed,

    /// <summary>
    ///     Declined.
    /// </summary>
    [EnumMember(Value = "DECLINED")] Declined
}