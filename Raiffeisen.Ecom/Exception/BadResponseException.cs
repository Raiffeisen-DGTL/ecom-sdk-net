using System;
using System.Net;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// Bad response from API.
/// </summary>
[Serializable]
[ComVisible(true)]
public class BadResponseException : System.Exception
{
    /// <summary>
    /// The response HTTP status code.
    /// </summary>
    public readonly HttpStatusCode HttpStatus;
        
    /// <summary>
    /// The response HTTP status code.
    /// </summary>
    public readonly IRawResponse? RawResponse;

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="rawResponse">The response data.</param>
    public BadResponseException(IRawResponse rawResponse)
        : base($"Http response code {rawResponse.HttpStatus}.")
    {
        HttpStatus = rawResponse.HttpStatus;
        RawResponse = rawResponse;
    }

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="httpStatus">The response HTTP status.</param>
    public BadResponseException(HttpStatusCode httpStatus)
        : base("Empty body, HTTP status " + httpStatus)
    {
        HttpStatus = httpStatus;
        RawResponse = null;
    }
}