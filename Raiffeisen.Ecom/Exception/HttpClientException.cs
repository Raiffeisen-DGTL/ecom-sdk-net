using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// The HTTP client error.
/// </summary>
[Serializable]
[ComVisible(true)]
public class HttpClientException : System.Exception
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="cause">The error cause.</param>
    public HttpClientException(System.Exception cause) : base("HTTP client error.", cause)
    {
    }
}