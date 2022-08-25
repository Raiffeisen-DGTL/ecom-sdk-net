using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Receipt item data.
/// </summary>
/// <typeparam name="TPaymentObject">Payment object type.</typeparam>
/// <typeparam name="TPaymentMode">Payment mode type.</typeparam>
/// <typeparam name="TMeasurementUnit">Measurement unit type.</typeparam>
/// <typeparam name="TVatType">Vat type.</typeparam>
/// <typeparam name="TAgentType">Agent type.</typeparam>
/// <typeparam name="TSupplierInfo">Supplier info type.</typeparam>
[ComVisible(true)]
public interface IItem<TPaymentObject, TPaymentMode, TMeasurementUnit, TVatType, TAgentType, TSupplierInfo>
    where TSupplierInfo : ISupplierInfo?
{
    /// <summary>
    ///     Name of goods, work, service, other subject of calculation.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Price per unit of goods, work, services, other subject of calculation in rubles.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Quantity/weight.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    ///     Amount in rubles. Should be equal to the product of price and quantity.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    ///     Sign of the subject of calculation.
    ///     If the parameter is not passed, it is filled with the default COMMODITY value.
    /// </summary>
    public TPaymentObject PaymentObject { get; set; }

    /// <summary>
    ///     Calculation method.
    /// </summary>
    public TPaymentMode PaymentMode { get; set; }

    /// <summary>
    ///     Unit of measurement of goods, work, services, other subject of calculation.
    /// </summary>
    public TMeasurementUnit MeasurementUnit { get; set; }

    /// <summary>
    ///     VAT rate per receipt item.
    /// </summary>
    public TVatType VatType { get; set; }

    /// <summary>
    ///     Sign of the agent on the subject of calculation.
    ///     An optional parameter that is filled only for operations through an agent.
    /// </summary>
    public TAgentType AgentType { get; set; }

    /// <summary>
    ///     Supplier information.
    ///     Required if the agentType parameter is filled in.
    /// </summary>
    public TSupplierInfo SupplierInfo { get; set; }
}