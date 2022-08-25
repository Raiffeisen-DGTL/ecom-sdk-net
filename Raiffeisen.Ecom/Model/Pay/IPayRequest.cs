using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay request interface.
/// </summary>
[ComVisible(true)]
public interface IPayRequest : IPay, IRequest
{
    /// <summary>
    ///     Structure with additional parameters from the store
    /// </summary>
    public dynamic? Extra { get; set; }
}