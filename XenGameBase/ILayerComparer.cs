using System.Collections.Generic;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public interface ILayerComparer : IComparer<ILayer> { }

    /// <summary>
    /// This comparer sorts IDrawable2D by DrawOrder.  It can be used to sort a collection comparers 
    /// so that they end in decrementing order.  
    /// </summary>
    /// <example>
    /// Unsorted DrawOrder Collection: {7, 2, 3, 1}
    /// Sorted DrawOrder Collection: {7, 3, 2, 1}
    /// </example>
    public class DecrementingLayerComparer : ILayerComparer
    {
        public static readonly DecrementingLayerComparer Instance = new DecrementingLayerComparer();

        private DecrementingLayerComparer() { }

        public int Compare( ILayer x, ILayer y )
        {
            return y.DrawOrder - x.DrawOrder;
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
    public class IncrementingLayerComparer : ILayerComparer
    {
        public static readonly IncrementingLayerComparer Instance = new IncrementingLayerComparer();

        private IncrementingLayerComparer() { }

        public int Compare( ILayer x, ILayer y )
        {
            return x.DrawOrder - y.DrawOrder;
        }
    }
}
