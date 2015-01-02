using XenAspects;
using Xen2D;
using Microsoft.Xna.Framework;

namespace XenGameBase
{
    public interface ISortedDecoratorCollection : ISortedPooledObjectNList<IElementDecorator>
    {
        void Add( IRenderable2D renderable, Vector2 anchorToParent );
        void Add( IRenderable2D renderable, Vector2 anchorToParent, Vector2 offset );
    }

    public class SortedDecoratorCollection : SortedPooledObjectNList<IElementDecorator>, ISortedDecoratorCollection
    {
        public SortedDecoratorCollection()
        {
            SortComparer = IncrementingDecoratorComparer.Instance;
        }

        public void Add( IRenderable2D renderable, Vector2 anchorToParent )
        {
            Add( renderable, anchorToParent, Vector2.Zero );
        }

        public void Add( IRenderable2D renderable, Vector2 anchorToParent, Vector2 offset )
        {
            Add( ElementDecorator.Acquire( renderable, anchorToParent, offset ) );
        }
    }
}
