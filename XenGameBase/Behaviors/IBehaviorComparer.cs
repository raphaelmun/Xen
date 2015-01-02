using System.Collections.Generic;

namespace XenGameBase
{
    public interface IBehaviorComparer : IComparer<IBehavior> { }

    /// <summary>
    /// This comparer sorts IDrawable2D by DrawOrder.  It can be used to sort a collection comparers 
    /// so that they end in decrementing order.  
    /// </summary>
    /// <example>
    /// Unsorted DrawOrder Collection: {7, 2, 3, 1}
    /// Sorted DrawOrder Collection: {7, 3, 2, 1}
    /// </example>
    public class DecrementingBehaviorComparer : IBehaviorComparer
    {
        public static readonly DecrementingBehaviorComparer Instance = new DecrementingBehaviorComparer();

        private DecrementingBehaviorComparer() { }

        public int Compare( IBehavior x, IBehavior y )
        {
            return y.UpdateOrder - x.UpdateOrder;
        }
    }

    /// <summary>
    /// This comparer sorts IDrawable2D by DrawOrder.  It can be used to sort a collection comparers 
    /// so that they end in incrementing order.  
    /// </summary>
    /// <example>
    /// Unsorted DrawOrder Collection: {7, 2, 3, 1}
    /// Sorted DrawOrder Collection: {1, 2, 3, 7}
    /// </example>
    public class IncrementingBehaviorComparer : IBehaviorComparer
    {
        public static readonly IncrementingBehaviorComparer Instance = new IncrementingBehaviorComparer();

        private IncrementingBehaviorComparer() { }

        public int Compare( IBehavior x, IBehavior y )
        {
            return x.UpdateOrder - y.UpdateOrder;
        }
    }
}
