using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingMatrixUtility
    {
        [TestMethod]
        public void GetExtentTransformWithDefaultValuesYieldsIdentityMatrix()
        {
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 1, 1 ), 0, Vector2.Zero );
            Assert.AreEqual( mat, Matrix.Identity );
        }

        [TestMethod]
        public void NonDefaultOriginYieldsExpectedTransform()
        {
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 1, 1 ), 0, new Vector2( 10, 15 ) );
            
            Vector2 result = Vector2.Transform( Vector2.Zero, mat );
            Assert.AreEqual( new Vector2( 10, 15 ), result );
        }

        [TestMethod]
        public void NonDefaultAngleYieldsExpectedTransform()
        {
            //angle of pi / 2 (90 degrees)
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 1, 1 ), MathHelper.PiOver2, Vector2.Zero );

            Vector2 result = VectorUtility.TransformAndRound( Vector2.UnitX, mat );
            Assert.AreEqual( Vector2.UnitY, result );
        }

        [TestMethod]
        public void NonDefaultScaleYieldsExpectedTransform()
        {
            //scale of (2,4)
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 2, 4 ), 0, Vector2.Zero );

            Vector2 result = VectorUtility.TransformAndRound( new Vector2( 1, 1 ), mat );
            Assert.AreEqual( new Vector2( 2, 4 ), result );
        }

        [TestMethod]
        public void ComplexTransformWorksCase1()
        {
            //translate to (5, 5), 
            //angle of pi / 2
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 1, 1 ), MathHelper.PiOver2, new Vector2( 5, 5 ) );

            Vector2 result = VectorUtility.TransformAndRound( Vector2.UnitX, mat );
            Assert.AreEqual( new Vector2( 5, 6 ), result );
        }

        [TestMethod]
        public void ComplexTransformWorksCase2()
        {
            //scale by (3, 3)
            //translate to (5, 5), 
            //angle of pi / 2
            Matrix mat = MatrixUtility.CreateTransform( new Vector2( 3, 3 ), MathHelper.PiOver2, new Vector2( 5, 5 ) );

            Vector2 result = VectorUtility.TransformAndRound( Vector2.UnitX, mat );
            Assert.AreEqual( new Vector2( 5, 8 ), result );
        }
    }
}
