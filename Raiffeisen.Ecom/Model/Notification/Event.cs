using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Notification;

/// <summary>
///     Event type.
/// </summary>
[ComVisible(true)]
public enum Event
{
    /// <summary>
    ///     Operation data.
    /// </summary>
    [EnumMember(Value = "payment")] Payment
}