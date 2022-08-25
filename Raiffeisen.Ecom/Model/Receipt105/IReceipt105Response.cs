using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Receipt v1.05 response interface.
/// </summary>
[ComVisible(true)]
public interface IReceipt105Response : IReceiptResponse<
    Customer, Item, PaymentObject?, PaymentMode?, string?, VatType?, AgentType?, SupplierInfo?, ReceiptType?, Status?,
    Payment, Type?
>
{
}