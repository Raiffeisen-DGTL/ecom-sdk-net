using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Transaction response interface.
/// </summary>
[ComVisible(true)]
public interface ITransactionResponse : IResponseWithCode<Code?>
{
    /// <summary>
    ///     Operation data.
    /// </summary>
    public Transaction? Transaction { get; set; }
}