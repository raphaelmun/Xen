using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingExtentIntersector
    {
        [TestMethod]
        public void NonIntersectingSimpleCaseWorks()
        {
            RectangularExtent extent1 = new RectangularExtent();
            extent1.Reset( 0, 0, 1, 1 );
            RectangularExtent extent2 = new RectangularExtent();
            extent2.Reset( 3, 4, 1, 1 );

            Assert.AreEqual( true, ExtentIntersector.IsDistanceBetweenCentersGreaterThanOuterRadiiSum( extent1, extent2 ) );
        }

        [TestMethod]
        public void IntersectingSimpleCaseWorks()
        {
            RectangularExtent extent1 = new RectangularExtent();
            extent1.Reset( 0, 0, 4, 4 );
            RectangularExtent extent2 = new RectangularExtent();
            extent2.Reset( 4, 0, 4, 4 );

            Assert.AreEqual( true, ExtentIntersector.IsDistanceBetweenCentersLessThanOrEqualInnerRadiiSum( extent1, extent2 ) );
        }

        [TestMethod]
        public void CollisionTypeInvalidWhenCollidingObjectWithItself()
        {
            ICollidableObject collidable = new MockCollidableObject();
            Assert.IsTrue( CollisionInteractionType.Invalid == CollisionChecker.GetCollisionInteractionType( collidable, collidable ) );
        }
    }
}
