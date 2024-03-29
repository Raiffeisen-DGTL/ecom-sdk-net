using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// Get payment form page.
    /// </summary>
    /// <param name="payParams">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The payment form response promise.</returns>
    public async Task<Model.Response.IRawResponse> GetPay(Model.Pay.PayParams payParams, string path = UriPaymentForm)
    {
        return await GetPay<Model.Pay.PayParams>(payParams, path);
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
        ExpandoObject extra = _converter.ReadValue<ExpandoObject>(_converter.WriteValue(payRequest.Extra ?? new object()));
        extra.TryAdd("apiClient", _fingerprint.GetClientName());
        extra.TryAdd("apiClientVersion", _fingerprint.GetClientVersion());
        payRequest.Extra = extra;
        IsValidOrThrow(payRequest);

        return await RequestRaw(
            "POST",
            JoinUriPath(path),
            payRequest
        );
    }

    /// <summary>
    /// Post payment form page.
    /// </summary>
    /// <param name="payRequest">The payment form params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The payment form response promise.</returns>
    public async Task<Model.Response.IRawResponse> PostPay(
        Model.Pay.PayRequest payRequest,
        string path = UriPaymentForm
    )
    {
        return await PostPay<Model.Pay.PayRequest>(payRequest, path);
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
    /// Getting information about the status of a transaction.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order transaction response promise.</returns>
    public async Task<Model.Transaction.ITransactionResponse> GetOrderTransaction(
        Model.Order.OrderParams orderParams,
        string path = UriPayments
    )
    {
        return await GetOrderTransaction<
            Model.Transaction.TransactionResponse,
            Model.Order.OrderParams
        >(orderParams, path);
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
    /// Processing a refund.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="refundRequest">The refund data.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order refund response promise.</returns>
    public async Task<Model.Refund.IRefundResponse> PostOrderRefund(
        Model.Refund.RefundParams refundParams,
        Model.Refund.RefundRequest refundRequest,
        string path = UriPayments
    )
    {
        return await PostOrderRefund<
            Model.Refund.RefundResponse,
            Model.Refund.RefundParams,
            Model.Refund.RefundRequest
        >(refundParams, refundRequest, path);
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
    /// Getting refund status.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order refund response promise.</returns>
    public async Task<Model.Refund.IRefundStatusResponse> GetOrderRefund(
        Model.Refund.RefundParams refundParams,
        string path = UriPayments
    )
    {
        return await GetOrderRefund<Model.Refund.RefundStatusResponse, Model.Refund.RefundParams>(refundParams, path);
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
    /// Getting order information.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order response promise.</returns>
    public async Task<Model.Order.IOrderResponse> GetOrder(
        Model.Order.OrderParams orderParams,
        string path = UriPayment
    )
    {
        return await GetOrder<Model.Order.OrderResponse, Model.Order.OrderParams>(orderParams, path);
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
    /// Delete order.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order response promise.</returns>
    public async Task<Model.Response.IRawResponse> DeleteOrder(
        Model.Order.OrderParams orderParams,
        string path = UriPayment
    )
    {
        return await DeleteOrder<Model.Order.OrderParams>(orderParams, path);
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
    /// Getting a list of v1.05 receipts.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order receipts list response promise.</returns>
    public async Task<Model.Receipt105.IReceipt105Response[]> GetOrderReceipts105(
        Model.Order.OrderParams orderParams,
        string path = UriFiscal
    )
    {
        return await GetOrderReceipts<Model.Receipt105.Receipt105Response, Model.Order.OrderParams>(orderParams, path);
    }
    
    /// <summary>
    /// Getting a list of v1.20 receipts.
    /// </summary>
    /// <param name="orderParams">The order params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order receipts list response promise.</returns>
    public async Task<Model.Receipt120.IReceipt120Response[]> GetOrderReceipts120(
        Model.Order.OrderParams orderParams,
        string path = UriFiscal
    )
    {
        return await GetOrderReceipts<Model.Receipt120.Receipt120Response, Model.Order.OrderParams>(orderParams, path);
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
    /// Getting order refund v1.05 receipt.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order refund receipt response promise.</returns>
    public async Task<Model.Receipt105.IReceipt105Response> GetOrderRefundReceipt105(
        Model.Refund.RefundParams refundParams,
        string path = UriFiscal
    )
    {
        return await GetOrderRefundReceipt<
            Model.Receipt105.Receipt105Response,
            Model.Refund.RefundParams
        >(refundParams, path);
    }
    
    /// <summary>
    /// Getting order refund v1.20 receipt.
    /// </summary>
    /// <param name="refundParams">The refund params.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The order refund receipt response promise.</returns>
    public async Task<Model.Receipt120.IReceipt120Response> GetOrderRefundReceipt120(
        Model.Refund.RefundParams refundParams,
        string path = UriFiscal
    )
    {
        return await GetOrderRefundReceipt<
            Model.Receipt120.Receipt120Response,
            Model.Refund.RefundParams
        >(refundParams, path);
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

    /// <summary>
    /// Setup callback URL.
    /// </summary>
    /// <param name="request">The request model.</param>
    /// <param name="path">The default path.</param>
    /// <returns>The response data promise.</returns>
    public async Task<Model.Callback.ICallbackResponse> PostCallbackUrl(
        Model.Callback.CallbackRequest request,
        string path = UriSettings
    )
    {
        return await PostCallbackUrl<
            Model.Callback.CallbackResponse,
            Model.Callback.CallbackRequest
        >(request, path);
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