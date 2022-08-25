using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund status response interface.
/// </summary>
[ComVisible(true)]
public interface IRefundStatusResponse : IRefundStatus, IResponseWithCode<Code?>
{
}