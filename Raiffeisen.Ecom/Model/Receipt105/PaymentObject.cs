using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt105;

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
    ///     Excisable goods.
    /// </summary>
    [EnumMember(Value = "EXCISE")] Excise,

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