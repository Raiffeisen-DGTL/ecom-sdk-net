using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Client;
    
/// <summary>
/// HTTP client interface.
/// </summary>
[ComVisible(true)]
public interface IClient
{
    /// <summary>
    /// Make HTTP request.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <param name="headers">The HTTP headers.</param>
    /// <param name="body">The request body.</param>
    /// <returns>The response data promise.</returns>
    Task<IRawResponse> Request(
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string? body = null
    );
}