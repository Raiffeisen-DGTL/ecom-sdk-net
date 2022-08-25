using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Payment params interface.
/// </summary>
[ComVisible(true)]
public interface IPaymentParams : IParams
{
    /// <summary>
    ///     Unique QRC identifier issued by the SBP.
    /// </summary>
    public string? QrId { get; set; }

    /// <summary>
    ///     Identification number.
    /// </summary>
    public decimal? Rrn { get; set; }

    /// <summary>
    ///     Payment authorization code received from the issuer.
    /// </summary>
    public decimal? AuthCode { get; set; }
}