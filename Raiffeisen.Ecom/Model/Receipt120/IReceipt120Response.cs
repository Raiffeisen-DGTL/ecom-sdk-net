using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Receipt v1.2 response interface.
/// </summary>
[ComVisible(true)]
public interface IReceipt120Response : IReceiptResponse<
    Customer, Item, PaymentObject?, PaymentMode?, MeasurementUnit?, VatType?, AgentType?, SupplierInfo?, ReceiptType?,
    Status?, Payment, Type?
>
{
}