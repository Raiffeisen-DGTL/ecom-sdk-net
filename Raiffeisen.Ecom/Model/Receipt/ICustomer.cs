using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Receipt customer data.
/// </summary>
[ComVisible(true)]
public interface ICustomer
{
    /// <summary>
    ///     E-mail of the buyer for sending a receipt.
    ///     If the E-mail is not sent, then the receipt is sent to the merchant's e-mail.
    /// </summary>
    public string Email { get; set; }
}