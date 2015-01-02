using Microsoft.Xna.Framework;

namespace XenScript
{
    /// <summary>
    /// Attempts to execute a command.
    /// </summary>
    public interface ICommandExecutor
    {
        bool CanExecute( ICommand command );
        bool CanExecuteImmediately( ICommand command );
        CommandExecutionResponse Execute( ICommand command );
        CommandExecutionResponse Enqueue( ICommand command );

        /// <summary>
        /// Cancels all outstanding actions.
        /// </summary>
        void CancelAll();

        /// <summary>
        /// Cancels all actions that use the specified capability
        /// </summary>
        /// <param name="capId">The capability Id to cancel.</param>
        void CancelCommandsThatUseCapability( int capId );
    }
}
