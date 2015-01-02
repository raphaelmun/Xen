using System.Collections.Generic;
using Xen2D;

namespace XenScript
{
    /// <summary>
    /// Represents an object that has capabilities and can execute actions corresponding to those capabilities.  
    /// </summary>
    public interface IActionExecutor : IUpdateableEx
    {
        /// <summary>
        /// Gets the list of capabilities that this ActionExecutor is capable of.
        /// </summary>
        List<int> Capabilities { get; }

        /// <summary>
        /// Gets or sets whether this action queue is enabled and actively processing actions.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Attempts to enqueue the action for processing in FIFO order.
        /// </summary>
        /// <param name="action">The action to process.</param>
        /// <returns>True if the required capabilities are available and the action can be processed.  False otherwise.</returns>
        bool Enqueue( IAction action );

        /// <summary>
        /// Attempts to immediately excute an action, cancelling any current actions that are 
        /// using the required capabilities.  
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>True if the required capabilities are available and the action can be processed.  False otherwise.</returns>
        bool Execute( IAction action );

        /// <summary>
        /// Cancels all currently executing actions and clears the action queue.
        /// </summary>
        void CancelAll();

        /// <summary>
        /// Cancels all actions (current or enqueued) that use specified capability.
        /// </summary>
        /// <param name="capId">The capability id to free up.</param>
        void CancelActionsThatUseCapability( int capId );
    }
}
