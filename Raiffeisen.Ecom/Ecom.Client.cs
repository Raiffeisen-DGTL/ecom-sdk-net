using System.Collections.Generic;
using System.Threading.Tasks;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
    /// <summary>
    /// Make the API request.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="BadResponseException">On response parse fail.</exception>
    /// <exception cref="ServiceException">On API return error message.</exception>
    private async Task<TResponse[]> RequestArray<TResponse>(
        string method,
        string url
    )
        where TResponse : Model.Response.IResponse
    {
        return await RequestArray<TResponse, Model.Response.IRequest>(method, url, null);
    }
    
    /// <summary>
    /// Make the API request with body.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <param name="request">The request body.</param>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="BadResponseException">On response parse fail.</exception>
    /// <exception cref="ServiceException">On API return error message.</exception>
    private async Task<TResponse[]> RequestArray<TResponse, TRequest>(
        string method,
        string url,
        TRequest? request
    )
        where TResponse : Model.Response.IResponse
        where TRequest : Model.Response.IRequest
    {
        return DeserializeResponseArrayBody<TResponse>(await RequestRaw(method, url, request));
    }
    
    /// <summary>
    /// Make the API request.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="BadResponseException">On response parse fail.</exception>
    /// <exception cref="ServiceException">On API return error message.</exception>
    private async Task<TResponse> Request<TResponse>(
        string method,
        string url
    )
        where TResponse : Model.Response.IResponse
    {
        return await Request<TResponse, Model.Response.IRequest>(method, url, null);
    }

    /// <summary>
    /// Make the API request with body.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <param name="request">The request body.</param>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    /// <exception cref="BadResponseException">On response parse fail.</exception>
    /// <exception cref="ServiceException">On API return error message.</exception>
    private async Task<TResponse> Request<TResponse, TRequest>(
        string method,
        string url,
        TRequest? request
    )
        where TResponse : Model.Response.IResponse
        where TRequest : Model.Response.IRequest
    {
        return DeserializeResponseBody<TResponse>(await RequestRaw(method, url, request));
    }

    /// <summary>
    /// Make raw the API request.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    private async Task<Model.Response.IRawResponse> RequestRaw(
        string method,
        string url
    )
    {
        return await RequestRaw<Model.Response.IRequest>(method, url, null);
    }
    
    /// <summary>
    /// Make raw the API request with body.
    /// </summary>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The endpoint URL.</param>
    /// <param name="request">The request body.</param>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <returns>The response HTTP body promise.</returns>
    /// <exception cref="SerializationException">On request body serialization fail.</exception>
    /// <exception cref="HttpClientException">On request fail.</exception>
    private async Task<Model.Response.IRawResponse> RequestRaw<TRequest>(
        string method,
        string url,
        TRequest? request
    )
        where TRequest : Model.Response.IRequest
    {
        var body = SerializeRequestBody(request);
        try
        {
            return await _client.Request(method, url, _headers, body);
        }
        catch (System.Exception exception)
        {
            throw new HttpClientException(exception);
        }
    }

    private string? SerializeRequestBody<TRequest>(TRequest? request = default)
        where TRequest : Model.Response.IRequest
    {
        try
        {
            return null != request ? _converter.WriteValue(request) : null;
        }
        catch (System.Exception exception)
        {
            throw new SerializationException(exception);
        }
    }

    private TResponse[] DeserializeResponseArrayBody<TResponse>(Model.Response.IRawResponse rawResponse)
        where TResponse : Model.Response.IResponse
    {
        if (string.IsNullOrEmpty(rawResponse.Body))
            throw new BadResponseException(rawResponse.HttpStatus);
        
        TResponse[] data;
        try
        {
            data = _converter.ReadValue<TResponse[]>(rawResponse.Body)!;
        }
        catch (System.Exception exception)
        {
            throw MapToError(rawResponse, exception);
        }

        return data;
    }
    
    private TResponse DeserializeResponseBody<TResponse>(Model.Response.IRawResponse rawResponse)
        where TResponse : Model.Response.IResponse
    {
        if (string.IsNullOrEmpty(rawResponse.Body))
            throw new BadResponseException(rawResponse.HttpStatus);

        TResponse data;
        try
        {
            data = _converter.ReadValue<TResponse>(rawResponse.Body)!;
        }
        catch (System.Exception exception)
        {
            throw MapToError(rawResponse, exception);
        }

        return data;
    }

    private System.Exception MapToError(Model.Response.IRawResponse rawResponse, System.Exception cause)
    {
        try
        {
            var errorResponse = _converter.ReadValue<Model.Response.ResponseError>(rawResponse.Body ?? "");
            if (errorResponse is null)
                return new SerializationException(cause);
            
            return new ServiceException(errorResponse, rawResponse.HttpStatus, cause);
        }
        catch (System.Exception exception)
        {
            return new SerializationException(exception);
        }
    }
}