using System;
using System.Threading.Tasks;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
    /// <summary>
    /// Get payment form page.
    /// </summary>
    /// <param name="payParams">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TParams">Pay params type.</typeparam>
    /// <returns>The payment form response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<Model.Response.IRawResponse> GetPay<TParams>(TParams payParams, string path = UriPaymentForm)
        where TParams : Model.Pay.IPayParams
    {
        IsNotAbstract(typeof(TParams));
        payParams.PublicId = _publicId;
        IsValidOrThrow(payParams);
        
        return await RequestRaw(
            "GET",
            GeneratePayUrl(payParams, path)
        );
    }

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
    public async Task<Model.Response.IRawResponse> PostPay<TRequest>(
        TRequest payRequest,
        string path = UriPaymentForm
    )
        where TRequest : Model.Pay.IPayRequest
    {
        IsNotAbstract(typeof(TRequest));
        payRequest.PublicId = _publicId;
        IsValidOrThrow(payRequest);
        
        return await RequestRaw(
            "POST",
            JoinUriPath(path),
            payRequest
        );
    }

    /// <summary>
    /// Getting information about the status of a transaction.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order transaction response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TResponse> GetOrderTransaction<TResponse, TParams>(TParams orderParams, string path = UriPayments)
        where TResponse : Model.Transaction.ITransactionResponse
        where TParams : Model.Order.IOrderParams
    {
        IsNotAbstract(typeof(TResponse));
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(orderParams);
        
        return await Request<TResponse>(
            "GET",
            JoinUriPath(path + "/orders/" + orderParams.OrderId + "/transaction")
        );
    }

    /// <summary>
    /// Processing a refund.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="refundRequest">The refund data.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TResponse">Result type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <returns>The order refund response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TResponse> PostOrderRefund<TResponse, TParams, TRequest>(
        TParams refundParams,
        TRequest refundRequest,
        string path = UriPayments
    )
        where TResponse : Model.Refund.IRefundResponse
        where TParams : Model.Refund.IRefundParams
        where TRequest : Model.Refund.IRefundRequest
    {
        IsNotAbstract(typeof(TResponse));
        IsNotAbstract(typeof(TParams));
        IsNotAbstract(typeof(TRequest));
        IsValidOrThrow(refundParams);
        IsValidOrThrow(refundRequest);
        
        return await Request<TResponse, TRequest>(
            "POST",
            JoinUriPath(path + "/orders/" + refundParams.OrderId + "/refunds/" + refundParams.RefundId ),
            refundRequest
        );
    }

    /// <summary>
    /// Getting refund status.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TResponse">Result type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order refund response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TResponse> GetOrderRefund<TResponse, TParams>(TParams refundParams, string path = UriPayments)
        where TResponse : Model.Refund.IRefundStatusResponse
        where TParams : Model.Refund.IRefundParams
    {
        IsNotAbstract(typeof(TResponse));
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(refundParams);
        
        return await Request<TResponse>(
            "GET",
            JoinUriPath(path + "/orders/" + refundParams.OrderId + "/refunds/" + refundParams.RefundId )
        );
    }

    /// <summary>
    /// Getting order information.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TResponse">Result type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TResponse> GetOrder<TResponse, TParams>(TParams orderParams, string path = UriPayment)
        where TResponse : Model.Order.IOrderResponse
        where TParams : Model.Order.IOrderParams
    {
        IsNotAbstract(typeof(TResponse));
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(orderParams);
        
        return await Request<TResponse>(
            "GET",
            JoinUriPath(path + "/orders/" + orderParams.OrderId )
        );
    }
    
    /// <summary>
    /// Delete order.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<Model.Response.IRawResponse> DeleteOrder<TParams>(TParams orderParams, string path = UriPayment)
        where TParams : Model.Order.IOrderParams
    {
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(orderParams);
        
        return await RequestRaw(
            "DELETE",
            JoinUriPath(path + "/orders/" + orderParams.OrderId )
        );
    }
    
    /// <summary>
    /// Getting a list of receipts.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TReceipt">Receipt type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order receipts list response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TReceipt[]> GetOrderReceipts<TReceipt, TParams>(TParams orderParams, string path = UriFiscal)
        where TReceipt : Model.Receipt.IReceiptResponseAny
        where TParams : Model.Order.IOrderParams
    {
        IsNotAbstract(typeof(TReceipt));
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(orderParams);
        
        return await RequestArray<TReceipt>(
            "GET",
            JoinUriPath(path + "/orders/" + orderParams.OrderId + "/receipts" )
        );
    }

    /// <summary>
    /// Getting order refund receipt.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TReceipt">Receipt type.</typeparam>
    /// <typeparam name="TParams">Params type.</typeparam>
    /// <returns>The order refund receipt response promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On serialisation error.</exception>
    /// <exception cref="UrlEncodingException">On URL build error.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TReceipt> GetOrderRefundReceipt<TReceipt, TParams>(TParams refundParams, string path = UriFiscal)
        where TReceipt : Model.Receipt.IReceiptResponseAny
        where TParams : Model.Refund.IRefundParams
    {
        IsNotAbstract(typeof(TReceipt));
        IsNotAbstract(typeof(TParams));
        IsValidOrThrow(refundParams);
        
        return await Request<TReceipt>(
            "GET",
            JoinUriPath(path + "/orders/" + refundParams.OrderId + "/refunds/" + refundParams.RefundId + "/receipt")
        );
    }

    /// <summary>
    /// Setup callback URL.
    /// </summary>
    /// <param name="request">The request model.</param>
    /// <param name="path">The default path.</param>
    /// <typeparam name="TResponse">Result data type.</typeparam>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <returns>The response data promise.</returns>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="BadResponseException">On response parse fail.</exception>
    /// <exception cref="ServiceException">On API return error message.</exception>
    /// <exception cref="ArgumentException">On abstract generic type.</exception>
    public async Task<TResponse> PostCallbackUrl<TResponse, TRequest>(
        TRequest request,
        string path = UriSettings
    )
        where TResponse : Model.Callback.ICallbackResponse
        where TRequest : Model.Callback.ICallbackRequest
    {
        IsNotAbstract(typeof(TResponse));
        IsNotAbstract(typeof(TRequest));
        IsValidOrThrow(request);
        
        return await Request<TResponse, TRequest>(
            "POST",
            JoinUriPath(path + "/callback"),
            request
        );
    }

    private string JoinUriPath(string path)
    {
        try
        {
            var uriBuilder = new UriBuilder(_host)
            {
                Path = path
            };

            return uriBuilder.Uri.ToString();
        }
        catch (System.Exception exception)
        {
            throw new UrlEncodingException(exception);
        }
    }
}