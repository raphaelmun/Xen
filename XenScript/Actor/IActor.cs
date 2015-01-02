using Microsoft.Xna.Framework;
using Xen2D;

namespace XenScript
{
    /// <summary>
    /// Represents an object that participates in the game world.  Accepts commands issued from a director.
    /// </summary>
    /// 
    /// <example>
    /// Units, switches, environment
    /// </example>
    public interface IActor : ICommandExecutor, IEntity, IUpdateableEx
    {
    }
}
