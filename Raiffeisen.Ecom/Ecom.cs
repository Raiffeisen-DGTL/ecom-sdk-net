using System.Collections.Generic;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Client;
using Raiffeisen.Ecom.Converter;
using Raiffeisen.Ecom.Fingerprint;
using Raiffeisen.Ecom.Validator;

namespace Raiffeisen.Ecom;

/// <summary>
/// API client.
/// </summary>
[ComVisible(true)]
public partial class Ecom
{
    private readonly string _secretKey;
    private readonly string _publicId;
    private readonly string _host;
    private readonly IFingerprint _fingerprint;
    private readonly IClient _client;
    private readonly IConverter _converter;
    private readonly IValidator _validator;
    private readonly Dictionary<string, string> _headers;

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="secretKey">The merchant secret key.</param>
    /// <param name="publicId">The merchant public ID.</param>
    /// <param name="host">The API host.</param>
    /// <param name="fingerprint">The client fingerprint.</param>
    /// <param name="client">The HTTP protocol mapper.</param>
    /// <param name="converter">The JSON converter.</param>
    /// <param name="validator">The validator.</param>
    private Ecom(
        string secretKey,
        string publicId,
        string host,
        IFingerprint fingerprint,
        IClient client,
        IConverter converter,
        IValidator validator
    )
    {
        _secretKey = secretKey;
        _publicId = publicId;
        _host = host;
        _fingerprint = fingerprint;
        _client = client;
        _converter = converter;
        _validator = validator;
        _headers = new Dictionary<string, string>
        {
            {"Content-Type", "application/json"},
            {"Accept", "application/json"},
            {"Authorization", "Bearer " + _secretKey},
            {"User-Agent", fingerprint.GetClientName() + '-' + fingerprint.GetClientVersion()}
        };
    }
}