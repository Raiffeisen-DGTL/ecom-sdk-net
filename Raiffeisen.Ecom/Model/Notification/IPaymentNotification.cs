using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Notification;

/// <summary>
///     Callback response interface.
/// </summary>
[ComVisible(true)]
public interface IPaymentNotification : INotification
{
    /// <summary>
    ///     Operation data.
    /// </summary>
    public Transaction.Transaction? Transaction { get; set; }
}