using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects_UnitTests;
using System.Collections.Generic;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingRectangularExtent
    {
        [TestMethod]
        public void ResetWorks()
        {
            RectangularExtent extent = new RectangularExtent();

            extent.Reset( new Vector2( 50, 100 ) );
            Assert.AreEqual( Vector2.Zero, extent.Origin );
            Assert.AreEqual( 50, extent.ReferenceWidth );
            Assert.AreEqual( 100, extent.ReferenceHeight );
        }

        [TestMethod]
        public void ResetWithOriginWorks()
        {
            RectangularExtent extent = new RectangularExtent();

            extent.Reset( new Vector2( 50, 100 ), new Vector2( 50, 50 ) );
            Assert.AreEqual( new Vector2( 50, 50 ), extent.Origin );
            Assert.AreEqual( new Vector2( 50, 50 ), extent.Anchor );
        }

        [TestMethod]
        public void BoundsAreCorrectAfterResetCase1()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 100, 100 ) );

            Assert.AreEqual( 0, extent.LowestX );
            Assert.AreEqual( 0, extent.LowestY );
            Assert.AreEqual( 100, extent.HighestX );
            Assert.AreEqual( 100, extent.HighestY );
        }

        [TestMethod]
        public void BoundsAreCorrectAfterResetCase2()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 100, 200 ) );
            extent.Scale *= 2;
            extent.Angle = MathHelper.PiOver2;

            Assert.AreEqual( -400, extent.LowestX );
            AssertHelper.AreApproximatelyEqual( 0, extent.LowestY );
            AssertHelper.AreApproximatelyEqual( 0, extent.HighestX );
            Assert.AreEqual( 200, extent.HighestY );
        }

        [TestMethod]
        public void InnerRadiusWithNoScaleYieldsExpectedValue()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 100, 200 ) );

            Assert.AreEqual( 50, extent.InnerRadius );
        }

        [TestMethod]
        public void InnerRadiusWithScaleYieldsExpectedValue()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 100, 200 ) );
            extent.Scale = new Vector2( 10, 2 );

            Assert.AreEqual( 200, extent.InnerRadius );
        }

        [TestMethod]
        public void OuterRadiusWithNoScaleYieldsExpectedValue()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 60, 80 ) );

            Assert.AreEqual( 50, extent.OuterRadius );
        }

        [TestMethod]
        public void OuterRadiusWithScaleYieldsExpectedValue()
        {
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 60, 80 ) );
            extent.Scale = new Vector2( 2, 2 );

            Assert.AreEqual( 100, extent.OuterRadius );
        }
    }
}
