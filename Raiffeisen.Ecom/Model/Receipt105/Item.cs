using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Item data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Item : IItem<PaymentObject?, PaymentMode?, string?, VatType?, AgentType?, SupplierInfo?>
{
    /// <summary>
    ///     Item code in hexadecimal notation with spaces or in GS1 DataMatrix format.
    ///     For example, '00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00'
    ///     or '010463003407001221CMK45BrhN0WLf'.
    ///     If you use the BIFIT online checkout, the nomenclature code can be transferred strictly only in hexadecimal
    ///     representation with spaces.
    /// </summary>
    [JsonPropertyName("nomenclatureCode")]
    [StringLength(150)]
    [CulturedRegularExpression(
        "^([a-fA-F0-9]{2}$)|(^([a-fA-F0-9]{2}\\s){1,31}[a-fA-F0-9]{2}|01(?<gtin>\\d{14})21(?<serial>[a-zA-Z0-9!\" % &'()*+\\/\\-.,:;=<>?_]{13})([a-zA-Z0-9!\" % &'()*+\\/\\- .,:;=<>?_]{1,119})?|(?<gtin1>\\d{14})(?<serial1>[a-zA-Z0-9!\" %&'()*+\\/\\-.,:;=<>?_]{11})[a-zA-Z0-9!\" %&'()*+\\/\\-.,:;=<>?_]{4})$")]
    public string? NomenclatureCode { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("name")]
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = default!;

    /// <inheritdoc />
    [JsonPropertyName("price")]
    [RequiredNotZero]
    [CulturedRegularExpression(@"^\d{1,8}(?:\.\d{1,2})?$")]
    public decimal Price { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("quantity")]
    [RequiredNotZero]
    [CulturedRegularExpression(@"^\d{1,5}(?:\.\d{1,3})?$")]
    public decimal Quantity { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("amount")]
    public decimal Amount => decimal.Round(decimal.Multiply(Price, Quantity), 2);

    /// <inheritdoc />
    [JsonPropertyName("paymentObject")]
    [JsonConverter(typeof(EnumConverter<PaymentObject>))]
    public PaymentObject? PaymentObject { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paymentMode")]
    [JsonConverter(typeof(EnumConverter<PaymentMode>))]
    public PaymentMode? PaymentMode { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("measurementUnit")]
    [StringLength(16)]
    public string? MeasurementUnit { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("vatType")]
    [JsonConverter(typeof(EnumConverter<VatType>))]
    [Required]
    public VatType? VatType { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("agentType")]
    [JsonConverter(typeof(EnumConverter<AgentType>))]
    public AgentType? AgentType { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("supplierInfo")]
    [RequiredIfNotNull("AgentType")]
    [RecursiveValidation]
    public SupplierInfo? SupplierInfo { get; set; }
}