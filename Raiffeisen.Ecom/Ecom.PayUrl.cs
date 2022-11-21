using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
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