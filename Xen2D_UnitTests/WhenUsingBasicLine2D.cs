using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingBasicLine2D
    {
        [TestMethod]
        public void CreatingNewLineHasExpectedStartAndStop()
        {
            IBasicLine2D line = new BasicLine2D( new Vector2( 2, 2 ), new Vector2( 2, 4 ) );

            Assert.AreEqual( line.Start, new Vector2( 2, 2 ) );
            Assert.AreEqual( line.Stop, new Vector2( 2, 4 ) );
            Assert.AreEqual( 2, line.Length );
        }

        [TestMethod]
        public void SetLengthYieldsExpectedResult()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, Vector2.Zero );
            line.Length = 5;

            Assert.AreEqual( line.Stop, new Vector2( 5, 0 ) );
        }

        [TestMethod]
        public void GetAngleYieldsExpectedResult()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, new Vector2( 0, 2 ) );
            Assert.IsTrue( XenMath.IsWithinToleranceToTarget( line.Angle, MathHelper.PiOver2, 0.001f ) );

            line = new BasicLine2D( Vector2.Zero, new Vector2( 0, -2 ) );
            Assert.IsTrue( XenMath.IsWithinToleranceToTarget( line.Angle, MathHelper.Pi * 1.5f, 0.001f ) );
        }

        [TestMethod]
        public void SetAngleYieldsExpectedResult()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, new Vector2( 2, 0 ) ); //angle 0
            line.Angle = MathHelper.Pi / 2;
            Assert.AreEqual( line.Stop, new Vector2( 0, 2 ) );
        }

        [TestMethod]
        public void RectWidthEqualsDistanceBetweenStartAndStop()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, new Vector2( 0, 2 ) );

            Assert.AreEqual( line.Length, 2 );
        }

        [TestMethod]
        public void ChangingStartYieldsCorrectDestinationRect()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, Vector2.Zero );
            line.Start = new Vector2( 0, 2 );
            Assert.AreEqual( line.Length, 2 );
        }

        [TestMethod]
        public void ChangingStopYieldsCorrectDestinationRect()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, Vector2.Zero );
            line.Stop = new Vector2( 0, 2 );
            Assert.AreEqual( line.Length, 2 );
        }

        [TestMethod]
        public void ReducingLengthToZeroPreservesAngle()
        {
            IBasicLine2D line = new BasicLine2D( Vector2.Zero, new Vector2( 5, 5 ) );
            float angle = line.Angle;
            line.Length = 0;
            Assert.AreEqual( angle, line.Angle );

            line.Length = 1;
            Assert.AreEqual( angle, line.Angle );
        }
    }
}
