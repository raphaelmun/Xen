using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace XenScript
{
    /// <summary>
    /// Interface for a command to perform an action or combination of actions.  Commands have required capabilities in order to be executed.  
    /// A Command is instantly accepted, queue'd or rejected.
    /// Ex: move to point A.  Cast spell.  Cancel casting spell.  
    /// </summary>
    public interface ICommand
    {
        List<int> RequiredCapabilities { get; }
        void Execute( IActionExecutor actionExecutor );
        void Enqueue( IActionExecutor actionExecutor );
    }
}
