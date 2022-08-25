using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Client;

/// <summary>
/// Factory for HTTP mapper.
/// </summary>
[ComVisible(true)]
public class ClientFactory : AbstractFactory<IClient, NetClient>
{
}