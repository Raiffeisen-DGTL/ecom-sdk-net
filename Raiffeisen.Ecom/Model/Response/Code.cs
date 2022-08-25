using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     HTTP request status code.
/// </summary>
[ComVisible(true)]
public enum Code
{
    /// <summary>
    ///     Success.
    /// </summary>
    [EnumMember(Value = "SUCCESS")] Success,

    /// <summary>
    ///     Error.
    /// </summary>
    [EnumMember(Value = "ERROR")] Error
}