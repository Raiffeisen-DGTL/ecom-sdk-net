using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt105;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay request interface witch receipt v1.05.
/// </summary>
/// <typeparam name="TReceipt">Receipt type.</typeparam>
[ComVisible(true)]
public interface IPayRequestReceipt105<TReceipt> : IPayRequest
    where TReceipt : IReceipt105Request
{
    /// <summary>
    ///     Receipt data.
    ///     The object must be passed if receipt fiscal is enabled.
    ///     If there is no receipt object, the receipt will not be created.
    /// </summary>
    public TReceipt? Receipt { get; set; }
}