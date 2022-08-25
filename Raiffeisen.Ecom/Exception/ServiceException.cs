using System;
using System.Net;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// The API error response.
/// </summary>
[Serializable]
[ComVisible(true)]
public class ServiceException : System.Exception
{
    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public readonly HttpStatusCode HttpStatus;

    /// <summary>
    /// The API response.
    /// </summary>
    public readonly IResponseError Response;

    /// <summary>
    ///     The constructor.
    /// </summary>
    /// <param name="response">The API response.</param>
    /// <param name="httpStatus">The HTTP status code.</param>
    /// <param name="cause">The error cause.</param>
    public ServiceException(
        IResponseError response,
        HttpStatusCode httpStatus,
        System.Exception cause
    ) : base(response.Message, cause)
    {
        Response = response;
        HttpStatus = httpStatus;
    }
}