using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Response;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Transaction response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class TransactionResponse : ITransactionResponse
{
    /// <inheritdoc />
    [JsonPropertyName("code")]
    [JsonConverter(typeof(EnumConverter<Code>))]
    public Code? Code { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("transaction")]
    [RecursiveValidation]
    public Transaction? Transaction { get; set; }
}