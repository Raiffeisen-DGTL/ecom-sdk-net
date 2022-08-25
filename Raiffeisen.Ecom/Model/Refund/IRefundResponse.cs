using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund response interface.
/// </summary>
[ComVisible(true)]
public interface IRefundResponse : IRefundStatus, IResponseWithCode<Code?>
{
}