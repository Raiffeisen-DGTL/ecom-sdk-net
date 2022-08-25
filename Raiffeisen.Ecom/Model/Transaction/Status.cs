using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Status data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Status
{
    /// <summary>
    ///     Transaction status.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonConverter(typeof(EnumConverter<Value>))]
    public Value? Value { get; set; }

    /// <summary>
    ///     Date and time of the event.
    /// </summary>
    [JsonPropertyName("date")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? Date { get; set; }
}