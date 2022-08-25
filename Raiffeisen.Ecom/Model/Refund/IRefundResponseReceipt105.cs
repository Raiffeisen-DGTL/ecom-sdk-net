using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt105;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund response interface.
/// </summary>
/// <typeparam name="TReceipt">Receipt type.</typeparam>
[ComVisible(true)]
public interface IRefundResponseReceipt105<TReceipt> : IRefundResponse
    where TReceipt : Receipt105Response
{
    /// <summary>
    ///     Receipt data.
    /// </summary>
    public TReceipt? Receipt { get; set; }
}