using System.Collections.Generic;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public interface IElement2DComparer : IComparer<IElement2D> { }

    /// <summary>
    /// This comparer sorts IDrawable2D by DrawOrder.  It can be used to sort a collection comparers 
    /// so that they end in decrementing order.  
    /// </summary>
    /// <example>
    /// Unsorted DrawOrder Collection: {7, 2, 3, 1}
    /// Sorted DrawOrder Collection: {7, 3, 2, 1}
    /// </example>
    public class DecrementingElement2DComparer : IElement2DComparer
    {
        public static readonly DecrementingElement2DComparer Instance = new DecrementingElement2DComparer();

        private DecrementingElement2DComparer() { }

        public int Compare( IElement2D x, IElement2D y )
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
    public class IncrementingElement2DComparer : IElement2DComparer
    {
        public static readonly IncrementingElement2DComparer Instance = new IncrementingElement2DComparer();

        private IncrementingElement2DComparer() { }

        public int Compare( IElement2D x, IElement2D y )
        {
            return x.DrawOrder - y.DrawOrder;
        }
    }
}
