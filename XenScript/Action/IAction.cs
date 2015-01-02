using System.Collections.Generic;
using Xen2D;
using XenAspects;

namespace XenScript
{
    /// <summary>
    /// Represents an action that the Actor wishes to take.  
    /// Ex: move, attack, turn, interact
    /// </summary>
    public interface IAction : IUpdateableEx
    {
        Event<IAction> OnActionStarted      { get; }
        Event<IAction> OnActionInterrupted  { get; }
        Event<IAction> OnActionFinished     { get; }

        List<int> RequiredCapabilities { get; }
        bool UsesCapability( int capId );
        ActionState State { get; }
        void Cancel();
    }

    public enum ActionState
    {
        NotStarted, //The action has not been processed yet.
        Processing, //The action is currently being processed.
        Stopped,    //The action was processing at one point, but was either interrupted or finished.
    }
}
