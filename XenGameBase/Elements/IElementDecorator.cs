using System.Collections.Generic;

namespace XenGameBase
{
    public interface IElementDecoratorComparer : IComparer<IElementDecorator> { }

    /// <summary>
    /// This comparer sorts IDrawable2D by DrawOrder.  It can be used to sort a collection comparers 
    /// so that they end in decrementing order.  
    /// </summary>
    /// <example>
    /// Unsorted DrawOrder Collection: {7, 2, 3, 1}
    /// Sorted DrawOrder Collection: {7, 3, 2, 1}
    /// </example>
    public class DecrementingDecoratorComparer : IElementDecoratorComparer
    {
        public static readonly DecrementingDecoratorComparer Instance = new DecrementingDecoratorComparer();

        private DecrementingDecoratorComparer() { }

        public int Compare( IElementDecorator x, IElementDecorator y )
        {
            return y.Component.UpdateOrder - x.Component.UpdateOrder;
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
    public class IncrementingDecoratorComparer : IElementDecoratorComparer
    {
        public static readonly IncrementingDecoratorComparer Instance = new IncrementingDecoratorComparer();

        private IncrementingDecoratorComparer() { }

        public int Compare( IElementDecorator x, IElementDecorator y )
        {
            return x.Component.UpdateOrder - y.Component.UpdateOrder;
        }
    }
}
