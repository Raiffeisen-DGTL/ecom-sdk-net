using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt105;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund request interface witch receipt v1.05.
/// </summary>
/// <typeparam name="TReceipt">Receipt type.</typeparam>
[ComVisible(true)]
public interface IRefundRequestReceipt105<
    TReceipt
> : IRefundRequest
    where TReceipt : IReceipt105Request
{
    /// <summary>
    ///     Receipt data.
    /// </summary>
    public TReceipt? Receipt { get; set; }
}