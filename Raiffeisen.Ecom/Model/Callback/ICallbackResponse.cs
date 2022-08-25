using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Callback;

/// <summary>
///     Callback response interface.
/// </summary>
[ComVisible(true)]
public interface ICallbackResponse : ICallback, IResponse
{
}