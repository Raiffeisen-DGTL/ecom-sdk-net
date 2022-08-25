using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt120;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund request interface witch receipt v1.2.
/// </summary>
/// <typeparam name="TReceipt">Receipt type.</typeparam>
[ComVisible(true)]
public interface IRefundRequestReceipt120<
    TReceipt
> : IRefundRequest
    where TReceipt : IReceipt120Request
{
    /// <summary>
    ///     Receipt data.
    /// </summary>
    public TReceipt? Receipt { get; set; }
}