using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Test.Client;

/// <summary>
/// Event arguments of request
/// </summary>
[ComVisible(true)]
public class RequestEventArgs : EventArgs
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="counter">The request counter.</param>
    /// <param name="rawResponse">The response data.</param>
    /// <param name="method">The request method.</param>
    /// <param name="url">The request path.</param>
    /// <param name="headers">The request headers.</param>
    /// <param name="body">The request body.</param>
    public RequestEventArgs(
        int counter,
        IRawResponse rawResponse,
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string body = null
    )
    {
        Counter = counter;
        RawResponse = rawResponse;
        Method = method;
        Url = url;
        Headers = headers;
        Body = body;
    }

    /// <summary>
    /// The request counter.
    /// </summary>
    public int Counter { get; }

    /// <summary>
    /// The response data.
    /// </summary>
    public IRawResponse RawResponse { get; }

    /// <summary>
    /// The request method.
    /// </summary>
    public string Method { get; }

    /// <summary>
    /// The request path.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// The request headers.
    /// </summary>
    public IReadOnlyDictionary<string, string> Headers { get; }

    /// <summary>
    /// The request body.
    /// </summary>
    public string Body { get; }
}