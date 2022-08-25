namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Payment details interface.
/// </summary>
/// <typeparam name="TType">Type type.</typeparam>
public interface IPayment<TType>
{
    /// <summary>
    ///     Payment type.
    /// </summary>
    public TType Type { get; set; }

    /// <summary>
    ///     Payment amount.
    /// </summary>
    public decimal Amount { get; set; }
}