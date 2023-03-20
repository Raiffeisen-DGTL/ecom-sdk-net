using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Web;
using Raiffeisen.Ecom.Exception;
using Raiffeisen.Ecom.Model.Pay;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
    private const string PayJs = @"({
        publicId,
        formData,
        url = 'https://e-commerce.raiffeisen.ru/pay',
        method = 'openPopup',
        sdk = 'PaymentPageSdk',
        src = 'https://pay.raif.ru/pay/sdk/v2/payment.styled.min.js'
    }) => new Promise((resolve, reject) => {
        const openPopup = () => {
            new this[sdk](publicId, {url})[method](formData).then(resolve).catch(reject);
        };
        if (!this.hasOwnProperty(sdk)) {
            const script = this.document.createElement('script');
            script.src = src;
            script.onload = openPopup;
            script.onerror = reject;
            this.document.head.appendChild(script);
        } else openPopup();
    })";
    
    /// <summary>
    /// Post payment form page.
    /// </summary>
    /// <param name="payRequest">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <returns>The payment form response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public string GetPayJs<TRequest>(
        TRequest payRequest,
        string path = UriPaymentForm
    )
        where TRequest : Model.Pay.IPayRequest
    {
        IsNotAbstract(typeof(TRequest));
        payRequest.PublicId = _publicId;
        payRequest.Extra = _converter.ReadValue<ExpandoObject>(_converter.WriteValue(payRequest.Extra ?? new object()));
        payRequest.Extra.apiClient ??= _fingerprint.GetClientName();
        payRequest.Extra.apiClientVersion ??= _fingerprint.GetClientVersion();
        IsValidOrThrow(payRequest);
        
        var jsRequest = new JsRequest<TRequest>
        {
            PublicId = _publicId,
            Url = JoinUriPath(path),
            FormData = payRequest
        };

        return $"({PayJs})({SerializeRequestBody(jsRequest)})";
    }
    
    /// <summary>
    /// Get payment from URL.
    /// </summary>
    /// <param name="payParams">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The payment form URL.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    public string GeneratePayUrl<TParams>(TParams payParams, string path = UriPaymentForm)
        where TParams : Model.Pay.IPayParams
    {
        IsNotAbstract(typeof(TParams));
        payParams.PublicId = _publicId;
        IsValidOrThrow(payParams);
        var queryParams = ToNameValueCollection(payParams);

        try
        {
            var uriBuilder = new UriBuilder(_host)
            {
                Path = path
            };
            var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            parameters.Add(queryParams);
            uriBuilder.Query = parameters.ToString();
            
            return uriBuilder.Uri.ToString();
        }
        catch (System.Exception e)
        {
            throw new UrlEncodingException(e);
        }
    }
    
    /// <summary>
    /// Get payment from URL.
    /// </summary>
    /// <param name="payParams">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The payment form URL.</returns>
    public string GeneratePayUrl(Model.Pay.PayParams payParams, string path = UriPaymentForm)
    {
        return GeneratePayUrl<>(payParams, path);
    }

    private NameValueCollection ToNameValueCollection(object value)
    {
        Dictionary<string, object?> jsonObject;
        try
        {
            jsonObject = _converter.ReadValue<Dictionary<string, object?>>(_converter.WriteValue(value))!;

        }
        catch (System.Exception exception)
        {
            throw new SerializationException(exception);
        }

        var collection = new NameValueCollection();
        foreach (var keyValuePair in jsonObject)
        {
            collection.Add(keyValuePair.Key, keyValuePair.Value?.ToString());
        }

        return collection;
    }
}