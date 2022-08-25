using System.Net;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Raw response interface.
/// </summary>
[ComVisible(true)]
public interface IRawResponse
{
    /// <summary>
    ///     The HTTP body.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    ///     The HTTP code.
    /// </summary>
    public HttpStatusCode HttpStatus { get; set; }
}