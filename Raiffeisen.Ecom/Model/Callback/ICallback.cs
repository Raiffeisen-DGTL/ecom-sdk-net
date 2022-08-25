using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Callback;

/// <summary>
///     Callback data interface.
/// </summary>
[ComVisible(true)]
public interface ICallback
{
    /// <summary>
    ///     URL for receiving notifications.
    /// </summary>
    public string CallbackUrl { get; set; }
}