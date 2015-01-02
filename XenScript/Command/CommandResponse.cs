using System;

namespace XenScript
{
    /// <summary>
    /// Result enumeration for when an Actor responds to the issuance of a command.
    /// </summary>
    public struct CommandResponse
    {
        public CommandExecutionResponse ExecutionResult;
        public CommandIssuanceResponse IssuanceResult;
    }
}
