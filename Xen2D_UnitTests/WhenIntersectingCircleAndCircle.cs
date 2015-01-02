using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenIntersectingCircleAndCircle
    {
        [TestMethod]
        public void CollisionBetweenTwoCPsFromSameExtentYieldsTrue()
        {
            CircularExtent extent = new CircularExtent();
            extent.Reset( Vector2.Zero, 50 );
            var co1 = new MockCollidableObject( extent );
            var co2 = new MockCollidableObject( extent );
            Assert.IsTrue( ExtentIntersector.AreInIntersectionCircleVsCircle( extent, extent ) );
        }

        [TestMethod]
        public void RectBoundPositiveCollisionCaseWorks()
        {
            //Tests to see if a circle in rect( 0, 0, 35, 35 ) intersects a circle bound by rect( 100, 100, 100, 100 )

            RectangularExtent extent1 = new RectangularExtent();
            extent1.Reset( new Vector2( 35, 35 ) );

            RectangularExtent extent2 = new RectangularExtent();
            extent2.Reset( new Vector2( 100, 100 ) );

            RectangularExtent extent3 = new RectangularExtent();
            extent3.Reset( new Vector2( 72, 72 ) );

            Assert.IsTrue( extent1.Intersects( CollisionMode.InnerCenterCircle, extent2, CollisionMode.InnerCenterCircle ) );
            Assert.IsTrue( extent1.Intersects( CollisionMode.InnerCenterCircle, extent3, CollisionMode.InnerCenterCircle ) );
        }
    }
}