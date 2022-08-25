using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Callback;

/// <summary>
///     Callback request interface.
/// </summary>
[ComVisible(true)]
public interface ICallbackRequest : ICallback, IRequest
{
}