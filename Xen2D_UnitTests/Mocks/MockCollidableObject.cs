using Xen2D;

namespace Xen2D_UnitTests
{
    class MockCollidableObject : CollidableObject<MockCollidableObject>
    {
        public MockCollidableObject() : this( null )
        {
        }

        public MockCollidableObject( IExtent extent )
            : this( extent, CollisionClasses.Default )
        {
        }

        public MockCollidableObject( IExtent extent, uint cc )
        {
            CollisionExtent = extent;
            CollisionClass = cc;
            CollisionMode = CollisionMode.Extent;
        }
    }
}
