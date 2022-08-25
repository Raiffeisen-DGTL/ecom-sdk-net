using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Measurement unit.
/// </summary>
[ComVisible(true)]
public enum MeasurementUnit
{
    /// <summary>
    ///     Piece/unit/fractional item.
    /// </summary>
    [EnumMember(Value = "PIECE")] Piece,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "GRAM")] Gram,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "KILOGRAM")] Kilogram,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "TON")] Ton,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "CENTIMETER")] Centimeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "DECIMETER")] Decimeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "METER")] Meter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "SQUARE_CENTIMETER")]
    SquareCentimeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "SQUARE_DECIMETER")]
    SquareDecimeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "SQUARE_METER")] SquareMeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "MILLILITER")] Milliliter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "LITER")] Liter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "CUBIC_METER")] CubicMeter,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "KILOWATT_HOUR")] KilowattHour,

    /// <summary>
    ///     Giga calorie.
    /// </summary>
    [EnumMember(Value = "GIGACALORIE")] GigaCalorie,

    /// <summary>
    ///     Day.
    /// </summary>
    [EnumMember(Value = "DAY")] Day,

    /// <summary>
    ///     Hour.
    /// </summary>
    [EnumMember(Value = "HOUR")] Hour,

    /// <summary>
    ///     Minute.
    /// </summary>
    [EnumMember(Value = "MINUTE")] Minute,

    /// <summary>
    ///     Second.
    /// </summary>
    [EnumMember(Value = "SECOND")] Second,

    /// <summary>
    ///     Kilobyte.
    /// </summary>
    [EnumMember(Value = "KILOBYTE")] Kilobyte,

    /// <summary>
    ///     Megabyte.
    /// </summary>
    [EnumMember(Value = "MEGABYTE")] Megabyte,

    /// <summary>
    ///     Giga byte.
    /// </summary>
    [EnumMember(Value = "GIGABYTE")] GigaByte,

    /// <summary>
    ///     Tera byte.
    /// </summary>
    [EnumMember(Value = "TERABYTE")] TeraByte,

    /// <summary>
    ///     Other
    /// </summary>
    [EnumMember(Value = "OTHER")] Other
}