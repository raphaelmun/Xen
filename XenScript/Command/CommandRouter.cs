using Microsoft.Xna.Framework;

namespace XenScript
{
    public class CommandRouter : ICommandRouter
    {
        /// <summary>
        /// Checks to see if the specified director can issue commands to the specified actor.
        /// </summary>
        /// <param name="director">The director that will be issuing the commands.</param>
        /// <param name="actor">The actor that will be receiving the commands.</param>
        /// <returns>True if the actor can accept commands from the director.  False otherwise.</returns>
        public bool CanIssue( IDirector director, IActor actor )
        {
            return true;
        }

        /// <summary>
        /// Issues a command from a director to an actor, which will cancel any existing commands if required capabilities are in use.
        /// </summary>
        /// <param name="director">The director that issued the command.</param>
        /// <param name="actor">The actor that is to execute the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The command issuance result.</returns>
        public CommandIssuanceResponse Issue( IDirector director, IActor actor, ICommand command )
        {
            return CommandIssuanceResponse.Accepted;
        }

        /// <summary>
        /// Enqueues a command from a director to an actor, which will add the command to the queue if required capabilities are in use.
        /// </summary>
        /// <param name="director">The director that issued the command.</param>
        /// <param name="actor">The actor that is to execute the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The command issuance result.</returns>
        public CommandIssuanceResponse Enqueue( IDirector director, IActor actor, ICommand command )
        {
            return CommandIssuanceResponse.Accepted;
        }

        protected bool CanCommand( IDirector director, IActor actor )
        {
            return true;
        }
    }
}
