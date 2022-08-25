using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Sign of the agent on the subject of calculation.
/// </summary>
[ComVisible(true)]
public enum AgentType
{
    /// <summary>
    ///     Bank payment agent.
    /// </summary>
    [EnumMember(Value = "BANK_PAYING_AGENT")]
    BankPayingAgent,

    /// <summary>
    ///     Bank payment subagent.
    /// </summary>
    [EnumMember(Value = "BANK_PAYING_SUBAGENT")]
    BankPayingSubagent,

    /// <summary>
    ///     Paying agent.
    /// </summary>
    [EnumMember(Value = "PAYING_AGENT")] PayingAgent,

    /// <summary>
    ///     Payment subagent.
    /// </summary>
    [EnumMember(Value = "PAYING_SUBAGENT")]
    PayingSubagent,

    /// <summary>
    ///     Attorney.
    /// </summary>
    [EnumMember(Value = "ATTORNEY")] Attorney,

    /// <summary>
    ///     Commission agent.
    /// </summary>
    [EnumMember(Value = "COMMISSION_AGENT")]
    CommissionAgent,

    /// <summary>
    ///     Another type of agent.
    /// </summary>
    [EnumMember(Value = "ANOTHER")] Another
}