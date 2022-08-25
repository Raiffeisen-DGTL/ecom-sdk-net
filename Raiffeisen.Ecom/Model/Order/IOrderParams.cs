using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Order params interface.
/// </summary>
[ComVisible(true)]
public interface IOrderParams : IParams
{
    /// <summary>
    ///     The order id.
    /// </summary>
    public string OrderId { get; set; }
}