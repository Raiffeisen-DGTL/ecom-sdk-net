using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Raiffeisen.Ecom.Client;
using Raiffeisen.Ecom.Converter;
using Raiffeisen.Ecom.Fingerprint;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Test.Client;

/// <summary>
/// Fake client.
/// </summary>
[ComVisible(true)]
public class FakeClient : IClient
{
    /// <summary>
    /// The data converter.
    /// </summary>
    public readonly IConverter Converter;

    /// <summary>
    /// The client fingerprint.
    /// </summary>
    public readonly IFingerprint Fingerprint;
    
    /// <summary>
    /// The constructor.
    /// </summary>
    public FakeClient() : this(new TextJsonConverter(), new Fingerprint.Fingerprint())
    {
    }

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="converter">The data converter.</param>
    /// <param name="fingerprint">The client fingerprint.</param>
    public FakeClient(IConverter converter, IFingerprint fingerprint)
    {
        Converter = converter;
        Fingerprint = fingerprint;
    }

    /// <summary>
    /// The counter of requests.
    /// </summary>
    public int RequestCounter { private get; set; }

    /// <summary>
    /// The response data.
    /// </summary>
    public IRawResponse RawResponse { private get; set; } = new RawResponse();

    /// <summary>
    /// Reset client end create Ecom.
    /// </summary>
    /// <param name="body">The response body.</param>
    /// <param name="httpStatus">The response HTTP status.</param>
    /// <returns>The Ecom instance.</returns>
    public Ecom Reset(string body = null, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        RawResponse = new RawResponse()
        {
            HttpStatus = httpStatus,
            Body = body
        };
        RequestCounter = 0;
        OnRequest = null;

        return Ecom.Create(
            "testSecretKey",
            "testPublicId",
            Ecom.HostTest,
            Fingerprint,
            this,
            Converter
        );
    }

    /// <inheritdoc />
    public async Task<IRawResponse> Request(
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string body = null
    )
    {
        var args = new RequestEventArgs(
            RequestCounter += 1,
            RawResponse,
            method,
            url,
            headers,
            body
        );
        OnRequest?.Invoke(this, args);
        await Task.Delay(0);
        return args.RawResponse;
    }

    /// <summary>
    /// Event of request.
    /// </summary>
    public event RequestHandler OnRequest;
}