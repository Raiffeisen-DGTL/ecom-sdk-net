using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Receipt v1.2 request interface.
/// </summary>
[ComVisible(true)]
public interface IReceipt120Request : IReceiptRequest<
    Customer, Item, PaymentObject?, PaymentMode?, MeasurementUnit?, VatType?, AgentType?, SupplierInfo?, Payment, Type?
>
{
}