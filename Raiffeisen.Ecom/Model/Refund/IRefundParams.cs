using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund params interface.
/// </summary>
[ComVisible(true)]
public interface IRefundParams : IParams
{
    /// <summary>
    ///     The order id.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    ///     The refund id.
    /// </summary>
    public string RefundId { get; set; }
}