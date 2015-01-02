using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;
using XenAspects;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionChecker
    {
        [TestMethod]
        public void CollisionTypeInvalidWhenCollidingObjectWithItself()
        {
            ICollidableObject collidable = new MockCollidableObject();
            Assert.IsTrue( CollisionInteractionType.Invalid == CollisionChecker.GetCollisionInteractionType( collidable, collidable ) );
        }

        [TestMethod]
        public void CollisionTypeInvalidWhenCollidingObjectWithNull()
        {
            ICollidableObject collidable = new MockCollidableObject();
            Assert.IsTrue( CollisionInteractionType.Invalid == CollisionChecker.GetCollisionInteractionType( collidable, null ) );
        }

        [TestMethod]
        public void CollisionTypeInvalidWhenCollidingNullWithNull()
        {
            ICollidableObject collidable = new MockCollidableObject();
            Assert.IsTrue( CollisionInteractionType.Invalid == CollisionChecker.GetCollisionInteractionType( null, null ) );
        }

        [TestMethod]
        public void CollisionTypeParsingYieldsExpectedResults()
        {
            Assert.IsTrue( CollisionInteractionType.ExtentVsExtent == CollisionChecker.GetCollisionInteractionType( CollisionMode.Extent, CollisionMode.Extent ) );
            Assert.IsTrue( CollisionInteractionType.ExtentVsPoint == CollisionChecker.GetCollisionInteractionType( CollisionMode.Extent, CollisionMode.CenterPoint ) );
            Assert.IsTrue( CollisionInteractionType.ExtentVsInnerCircle == CollisionChecker.GetCollisionInteractionType( CollisionMode.Extent, CollisionMode.InnerCenterCircle ) );
            Assert.IsTrue( CollisionInteractionType.InnerCircleVsInnerCircle == CollisionChecker.GetCollisionInteractionType( CollisionMode.InnerCenterCircle, CollisionMode.InnerCenterCircle ) );
            Assert.IsTrue( CollisionInteractionType.InnerCircleVsPoint == CollisionChecker.GetCollisionInteractionType( CollisionMode.InnerCenterCircle, CollisionMode.CenterPoint ) );
            Assert.IsTrue( CollisionInteractionType.PointVsPoint == CollisionChecker.GetCollisionInteractionType( CollisionMode.CenterPoint, CollisionMode.CenterPoint ) );
        }

        [TestMethod]
        public void LessThanMinimumRadiusYieldsIntersectionIsTrue()
        {
            RectangularExtent re1 = RectangularExtent.Acquire();
            re1.Reset( 0, 0, 50, 20 );
            RectangularExtent re2 = RectangularExtent.Acquire();
            re2.Reset( 0, 0, 50, 20 );

            bool intersects;
            Assert.IsTrue( ExtentIntersector.IsSimpleIntersectionCase( re1, re2, out intersects ) );
            Assert.IsTrue( intersects );
        }

        [TestMethod]
        public void GreaterThanMaximumRadiusYieldsIntersectionIsFalse()
        {
            RectangularExtent re1 = RectangularExtent.Acquire();
            re1.Reset( 0, 0, 50, 20 );
            RectangularExtent re2 = RectangularExtent.Acquire();
            re2.Reset( 0, 70, 50, 20 );

            bool intersects;
            Assert.IsTrue( ExtentIntersector.IsSimpleIntersectionCase( re1, re2, out intersects ) );
            Assert.IsFalse( intersects );
        }

        [TestMethod]
        public void ComplexRadiusYieldsIntersectionIsFalse()
        {
            RectangularExtent re1 = RectangularExtent.Acquire();
            re1.Reset( 0, 0, 50, 20 );
            RectangularExtent re2 = RectangularExtent.Acquire();
            re2.Reset( 0, 30, 50, 20 );

            bool intersects;
            Assert.IsFalse( ExtentIntersector.IsSimpleIntersectionCase( re1, re2, out intersects ) );
            Assert.IsFalse( intersects );
        }
    }
}
