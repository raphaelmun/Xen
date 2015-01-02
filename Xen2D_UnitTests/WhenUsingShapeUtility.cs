using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingShapeUtility
    {
        [TestMethod]
        public void ContainedCirclesYieldTrueForIntersection()
        {
            ICircle2D circleA = ShapeCircle.Acquire( new Vector2( 100, 100 ), 40 );
            ICircle2D circleB = ShapeCircle.Acquire( new Vector2( 100, 100 ), 20 );

            Assert.IsTrue( ShapeUtility.Intersects( circleA, circleB ) );
        }
    }
}
