using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Subject of calculation.
/// </summary>
[ComVisible(true)]
public enum PaymentObject
{
    /// <summary>
    ///     Product.
    /// </summary>
    [EnumMember(Value = "COMMODITY")] Commodity,

    /// <summary>
    ///     Goods that are not excisable are subject to labeling, but do not have a labeling code.
    /// </summary>
    [EnumMember(Value = "COMMODITY_MARKING_NO_CODE")]
    CommodityMarkingNoCode,

    /// <summary>
    ///     Goods that are not excisable are subject to labeling and have a labeling code.
    /// </summary>
    [EnumMember(Value = "COMMODITY_MARKING_WITH_CODE")]
    CommodityMarkingWithCode,

    /// <summary>
    ///     Excisable goods that are not subject to labeling.
    /// </summary>
    [EnumMember(Value = "EXCISE")] Excise,

    /// <summary>
    ///     Excisable goods that are subject to labeling but do not have a labeling code.
    /// </summary>
    [EnumMember(Value = "EXCISE_MARKING_NO_CODE")]
    ExciseMarkingNoCode,

    /// <summary>
    ///     Excisable goods that are subject to labeling and have a labeling code.
    /// </summary>
    [EnumMember(Value = "EXCISE_MARKING_WITH_CODE")]
    ExciseMarkingWithCode,

    /// <summary>
    ///     Job.
    /// </summary>
    [EnumMember(Value = "JOB")] Job,

    /// <summary>
    ///     Service.
    /// </summary>
    [EnumMember(Value = "SERVICE")] Service,

    /// <summary>
    ///     Payment.
    /// </summary>
    [EnumMember(Value = "PAYMENT")] Payment,

    /// <summary>
    ///     Other subject of calculation.
    /// </summary>
    [EnumMember(Value = "ANOTHER")] Another
}