using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Receipt registration status.
/// </summary>
[ComVisible(true)]
public enum Status
{
    /// <summary>
    ///     Receipt draft created.
    /// </summary>
    [EnumMember(Value = "NEW")] New,

    /// <summary>
    ///     Receipt in the process of registration in OFD.
    /// </summary>
    [EnumMember(Value = "IN_PROGRESS")] InProgress,

    /// <summary>
    ///     Receipt successfully registered in OFD.
    /// </summary>
    [EnumMember(Value = "DONE")] Done,

    /// <summary>
    ///     Receipt registration in OFD ended with an error.
    /// </summary>
    [EnumMember(Value = "FAILED")] Failed,

    /// <summary>
    ///     Technical status before sending a receipt for registration to OFD.
    /// </summary>
    [EnumMember(Value = "AWAITING")] Awaiting
}