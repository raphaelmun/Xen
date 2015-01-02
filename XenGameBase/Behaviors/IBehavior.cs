using Xen2D;
using XenAspects;

namespace XenGameBase
{
    /// <summary>
    /// Interface for a behavior, which represents a dynamic aspect of an element.  
    /// Behaviors can be drawn and/or updated.  They can be used to change properties 
    /// on the decorated parent over time.  
    /// </summary>
    public interface IBehavior : IComponent2D, IAcceptsInput
    {
        /// <summary>
        /// The element to which the Behavior applies.  A behavior instance can only ever apply to a single parent element.  
        /// </summary>
        IElement2D Parent { get; set; }
    }
}
