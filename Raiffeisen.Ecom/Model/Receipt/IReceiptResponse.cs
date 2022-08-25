using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Receipt response interface.
/// </summary>
/// <typeparam name="TCustomer">Customer type.</typeparam>
/// <typeparam name="TItem">Item type.</typeparam>
/// <typeparam name="TPaymentObject">Payment object type.</typeparam>
/// <typeparam name="TPaymentMode">Payment mode type.</typeparam>
/// <typeparam name="TMeasurementUnit">Measurement unit type.</typeparam>
/// <typeparam name="TVatType">Vat type.</typeparam>
/// <typeparam name="TAgentType">Agent type.</typeparam>
/// <typeparam name="TSupplierInfo">Supplier info type.</typeparam>
/// <typeparam name="TReceiptType">Receipt type.</typeparam>
/// <typeparam name="TStatus">Status type.</typeparam>
/// <typeparam name="TPayment">Payment type</typeparam>
/// <typeparam name="TType">Type type.</typeparam>
[ComVisible(true)]
public interface IReceiptResponse<
    TCustomer, TItem, TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo, TReceiptType,
    TStatus, TPayment, TType
> : IResponse, IReceipt<
    TCustomer, TItem, TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo, TPayment,
    TType
>
    where TCustomer : ICustomer
    where TItem : IItem<TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo>
    where TSupplierInfo : ISupplierInfo?
    where TPayment : IPayment<TType?>
{
    /// <summary>
    ///     Receipt type.
    /// </summary>
    public TReceiptType ReceiptType { get; set; }

    /// <summary>
    ///     Receipt type.
    /// </summary>
    public TStatus Status { get; set; }

    /// <summary>
    ///     Order ID.
    /// </summary>
    public string? OrderNumber { get; set; }

    /// <summary>
    ///     Order ID.
    /// </summary>
    public decimal? Total { get; set; }
}