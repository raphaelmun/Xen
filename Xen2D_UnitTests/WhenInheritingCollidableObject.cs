using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenInheritingCollidableObject
    {
        [TestMethod]
        public void DefaultCollisionClassIsExpectedValue()
        {
            var mco = new MockCollidableObject( new CircularExtent() );
            Assert.AreEqual( CollisionClasses.Default, mco.CollisionClass );
        }

        [TestMethod]
        public void CollidableObjectDoesNotIntersectNullOther()
        {
            var mco = new MockCollidableObject( new CircularExtent() );
            Assert.IsFalse( mco.Intersects( null ) );
        }

        [TestMethod]
        public void CollidableObjectWithNullExtentDoesNotIntersectOtherWithNonNullExtent()
        {
            var mco1 = new MockCollidableObject( null );
            var mco2 = new MockCollidableObject( new CircularExtent() );
            Assert.IsFalse( mco1.Intersects( mco2 ) );
        }
    }
}
