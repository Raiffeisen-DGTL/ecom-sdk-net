using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund request interface.
/// </summary>
[ComVisible(true)]
public interface IRefundRequest : IRequest
{
    /// <summary>
    ///     Refund amount in rubles.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Refund amount in rubles.
    /// </summary>
    public string? PaymentDetails { get; set; }
}