using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay params interface.
/// </summary>
[ComVisible(true)]
public interface IPayParams : IPay, IParams
{
}