using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Response with code interface.
/// </summary>
/// <typeparam name="TCode">The code type.</typeparam>
[ComVisible(true)]
public interface IResponseWithCode<TCode> : IResponse
{
    /// <summary>
    ///     HTTP request status code.
    /// </summary>
    public TCode? Code { get; set; }
}