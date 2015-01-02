using Microsoft.Xna.Framework;

namespace XenScript
{    
    /// <summary>
    /// Interface for a Condition.  A condition checks to see that a set of observations about a subject are true.
    /// </summary>
    public interface ICondition
    {
        bool Evaluate( ref GameTime gt );
    }

    public abstract class Condition : ICondition
    {
        public abstract bool Evaluate( ref GameTime gt );
    }

    public class AlwaysTrueCondition : Condition
    {
        public override bool Evaluate( ref GameTime gt )
        {
            return true;
        }
    }
}
