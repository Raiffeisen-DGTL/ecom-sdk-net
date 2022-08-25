namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund status interface.
/// </summary>
public interface IRefundStatus
{
    /// <summary>
    ///     Amount in rubles.
    ///     Should be equal to the product of price and quantity.
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    ///     Return request status code.
    /// </summary>
    public RefundStatus? RefundStatus { get; set; }
}