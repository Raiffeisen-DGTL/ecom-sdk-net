using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Receipt v1.05 request interface.
/// </summary>
[ComVisible(true)]
public interface IReceipt105Request : IReceiptRequest<
    Customer, Item, PaymentObject?, PaymentMode?, string?, VatType?, AgentType?, SupplierInfo?, Payment, Type?
>
{
}