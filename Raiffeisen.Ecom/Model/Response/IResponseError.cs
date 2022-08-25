using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Response error interface.
/// </summary>
[ComVisible(true)]
public interface IResponseError : IResponseWithCode<string?>
{
    /// <summary>
    ///     Error description.
    /// </summary>
    public string? Message { get; set; }
}