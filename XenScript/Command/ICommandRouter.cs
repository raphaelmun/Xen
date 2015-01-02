using Microsoft.Xna.Framework;

namespace XenScript
{
    /// <summary>
    /// Interface for a class that processes and routes commands from directors to actors.
    /// </summary>
    public interface ICommandRouter
    {
        /// <summary>
        /// Checks to see if the specified director can issue commands to the specified actor.
        /// </summary>
        /// <param name="director">The director that will be issuing the commands.</param>
        /// <param name="actor">The actor that will be receiving the commands.</param>
        /// <returns>True if the actor can accept commands from the director.  False otherwise.</returns>
        bool CanIssue( IDirector director, IActor actor );

        /// <summary>
        /// Issues a command from a director to an actor, which will cancel any existing commands if required capabilities are in use.
        /// </summary>
        /// <param name="director">The director that issued the command.</param>
        /// <param name="actor">The actor that is to execute the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The command issuance result.</returns>
        CommandIssuanceResponse Issue( IDirector director, IActor actor, ICommand command );

        /// <summary>
        /// Enqueues a command from a director to an actor, which will add the command to the queue if required capabilities are in use.
        /// </summary>
        /// <param name="director">The director that issued the command.</param>
        /// <param name="actor">The actor that is to execute the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The command issuance result.</returns>
        CommandIssuanceResponse Enqueue( IDirector director, IActor actor, ICommand command );
    }
}
