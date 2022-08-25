using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Notification;

/// <summary>
///     Notification interface.
/// </summary>
[ComVisible(true)]
public interface INotification
{
    /// <summary>
    ///     Message type.
    /// </summary>
    public Event? Event { get; set; }
}