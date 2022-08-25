using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt120;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund response interface.
/// </summary>
/// <typeparam name="TReceipt">Receipt type.</typeparam>
[ComVisible(true)]
public interface IRefundResponseReceipt120<TReceipt> : IRefundResponse
    where TReceipt : IReceipt120Response
{
    /// <summary>
    ///     Receipt data.
    /// </summary>
    public TReceipt? Receipt { get; set; }
}