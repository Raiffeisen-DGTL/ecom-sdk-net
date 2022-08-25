using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Receipt;

/// <summary>
///     Supplier info interface.
/// </summary>
[ComVisible(true)]
public interface ISupplierInfo
{
    /// <summary>
    ///     Supplier phone.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     Supplier name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Supplier TIN.
    /// </summary>
    public string Inn { get; set; }
}