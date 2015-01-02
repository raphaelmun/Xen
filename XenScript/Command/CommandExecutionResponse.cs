using System;

namespace XenScript
{
    /// <summary>
    /// Result enumeration for when an actor tries to execute a command. 
    /// </summary>
    public enum CommandExecutionResponse : uint
    {
        Invalid,            //uninitialized default state
        Incapable,          //actor does not know how to execute (lacks capability)
        AbilityDisabled,    //actor could normally execute this command but is disabled
        ActorDisabled,      //actor is disabled and cannot execute this command
        ExecutingNow,       //actor accepted the command and is executing it immediately
        ExecutingLater,     //actor has queued the command and will execute it later.  
    }

    public enum CommandExecutionResult : uint
    {
        /// <summary>
        /// Default uninitialized state.
        /// </summary>
        Invalid,

        /// <summary>
        /// The command completed successfully 
        /// </summary>
        Completed,

        /// <summary>
        /// The command was interrupted.
        /// </summary>
        Interrupted,

        /// <summary>
        /// The command was cancelled.
        /// </summary>
        Cancelled,
    }
}
