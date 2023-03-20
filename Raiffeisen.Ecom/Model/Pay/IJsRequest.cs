using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     JS pay form request.
/// </summary>
/// <typeparam name="TPayRequest">Request type.</typeparam>
[ComVisible(true)]
public interface IJsRequest<TPayRequest> : IRequest
    where TPayRequest : IPayRequest
{
    /// <summary>
    ///     The merchant public ID.
    /// </summary>
    public string PublicId { get; set; }

    /// <summary>
    ///     The pay from URL.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     The original pay form request.
    /// </summary>
    public TPayRequest FormData { get; set; }
}
