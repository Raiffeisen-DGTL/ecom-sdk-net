using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Item data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Item : IItem<PaymentObject?, PaymentMode?, MeasurementUnit?, VatType?, AgentType?, SupplierInfo?>
{
    /// <summary>
    ///     Marking data.
    /// </summary>
    [JsonPropertyName("marking")]
    [RequiredIfValue("PaymentObject", Receipt120.PaymentObject.CommodityMarkingWithCode)]
    [RequiredIfValue("PaymentObject", Receipt120.PaymentObject.ExciseMarkingWithCode)]
    [RecursiveValidation]
    public Marking? Marking { get; set; }

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
    [JsonConverter(typeof(EnumConverter<MeasurementUnit>))]
    public MeasurementUnit? MeasurementUnit { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("vatType")]
    [Required]
    [JsonConverter(typeof(EnumConverter<VatType>))]
    public VatType? VatType { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("agentType")]
    [JsonConverter(typeof(EnumConverter<AgentType>))]
    public AgentType? AgentType { get; set; }

    /// <summary>
    ///     Supplier information.
    ///     Required if the agentType parameter is filled in.
    /// </summary>
    [JsonPropertyName("supplierInfo")]
    [RequiredIfNotNull("AgentType")]
    [RecursiveValidation]
    public SupplierInfo? SupplierInfo { get; set; }
}