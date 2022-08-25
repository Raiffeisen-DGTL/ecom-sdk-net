using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Receipt interface.
/// </summary>
/// <typeparam name="TCustomer">Customer type.</typeparam>
/// <typeparam name="TItem">Item type</typeparam>
/// <typeparam name="TPaymentObject">Payment object type.</typeparam>
/// <typeparam name="TPaymentMode">Payment mode type.</typeparam>
/// <typeparam name="TMeasurementUnit">Measurement unit type.</typeparam>
/// <typeparam name="TVatType">Vat type.</typeparam>
/// <typeparam name="TAgentType">Agent type.</typeparam>
/// <typeparam name="TSupplierInfo">Supplier info type.</typeparam>
/// <typeparam name="TPayment">Payment type</typeparam>
/// <typeparam name="TType">Type type.</typeparam>
[ComVisible(true)]
public interface IReceipt<
    TCustomer, TItem, TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo, TPayment,
    TType
>
    where TCustomer : ICustomer
    where TItem : IItem<TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo>
    where TSupplierInfo : ISupplierInfo?
    where TPayment : IPayment<TType?>
{
    /// <summary>
    ///     Unique receipt number.
    ///     If the value is not passed, then the order number (orderId) is accepted.
    /// </summary>
    public string? ReceiptNumber { get; set; }
    
    /// <summary>
    ///     Buyer data.
    /// </summary>
    public TCustomer? Customer { get; set; }

    /// <summary>
    ///     Receipt positions.
    /// </summary>
    public TItem[] Items { get; set; }

    /// <summary>
    ///     Payment details.
    /// </summary>
    public TPayment[]? Payments { get; set; }
}