using System;

namespace XenScript
{
    /// <summary>
    /// Result enumeration for when an Actor responds to the issuance of a command.
    /// </summary>
    public enum CommandIssuanceResponse : uint
    {
        Invalid,            //uninitialized default state
        CannotCommand,      //actor does not listen to commands from this director
        AlreadyControlled,  //actor is under control by a higher priority director
        Accepted,           //actor accepted the command
    }
}
