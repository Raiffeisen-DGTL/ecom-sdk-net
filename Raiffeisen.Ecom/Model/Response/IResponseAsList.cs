using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Response as list interface.
/// </summary>
/// <typeparam name="TItem">Item data.</typeparam>
[ComVisible(true)]
public interface IResponseAsList<TItem> : IResponse, IList<TItem>
    where TItem : new()
{
}