using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCircularExtent
    {
        [TestMethod]
        public void SimpleCircleHasExpectedBounds()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            Assert.AreEqual( 0, extent.LowestX );
            Assert.AreEqual( 0, extent.LowestY );
            Assert.AreEqual( 200, extent.HighestX );
            Assert.AreEqual( 200, extent.HighestY );
        }

        [TestMethod]
        public void ScaledRadiusYieldsExpectedResult()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            Assert.AreEqual( 100, extent.Radius );
            extent.Scale = new Vector2( 2, 2 );
            Assert.AreEqual( 200, extent.Radius );
        }

        [TestMethod]
        public void SettingScaleTwoUnequalXAndYThrowsException()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );

            try
            {
                extent.Scale = new Vector2( 2, 1 );
                Assert.Fail( "should not have reached here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void ScaledBoundsYieldExpectedResults()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            Assert.AreEqual( 100, extent.Radius );
            extent.Scale = new Vector2( 2, 2 );
            Assert.AreEqual( -100, extent.LowestX );
            Assert.AreEqual( -100, extent.LowestY );
            Assert.AreEqual( 300, extent.HighestX );
            Assert.AreEqual( 300, extent.HighestY );
        }

        [TestMethod]
        public void CircularExtentHasOneSide()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            Assert.AreEqual( 1, extent.NumSides );
        }

        [TestMethod]
        public void FindClosestPointWorks()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            Assert.AreEqual( new Vector2( 100, 0 ), extent.FindClosestPoint( new Vector2( 100, -50 ) ) );
            Assert.AreEqual( new Vector2( 200, 100 ), extent.FindClosestPoint( new Vector2( 300, 100 ) ) );
            Assert.AreEqual( new Vector2( 100, 200 ), extent.FindClosestPoint( new Vector2( 100, 300 ) ) );
            Assert.AreEqual( new Vector2( 0, 100 ), extent.FindClosestPoint( new Vector2( -100, 100 ) ) );

            float edgeLength = (float) ( 100 / Math.Pow( 2, 0.5 ) );
            edgeLength += 100;
            Assert.AreEqual( new Vector2( edgeLength, edgeLength ), extent.FindClosestPoint( new Vector2( 400, 400 ) ) );
        }

        [TestMethod]
        public void ReferenceRadiusYieldsExepctedResult()
        {
            CircularExtent extent = CircularExtent.Acquire( new Vector2( 100, 100 ), 100 );
            extent.Scale = new Vector2( 2, 2 );
            Assert.AreEqual( 100, extent.ReferenceRadius );
        }
    }
}
