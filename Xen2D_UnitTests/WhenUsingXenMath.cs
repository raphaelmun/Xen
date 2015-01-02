using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingXenMath
    {
        [TestMethod]
        public void WithinToleranceShouldForFloatShouldWork()
        {
            //Arrange
            float f             = 1.000001f;
            float target        = 1.0f;
            float tolerance     = 0.0000001f;
            float tolerance2    = 0.00001f;

            //Act

            //Assert
            Assert.IsFalse( XenMath.IsWithinToleranceToTarget( f, target, tolerance ) );
            Assert.IsTrue( XenMath.IsWithinToleranceToTarget( f, target, tolerance2 ) );
        }

        [TestMethod]
        public void RotateHorizontalVector90DegreesShouldResultInVerticalVector()
        {
            //Arrange
            Vector2 original = new Vector2( 1, 0 );

            //Act
            Vector2 rotated = XenMath.Rotate( original, MathHelper.Pi / 2 );

            //Assert
            Assert.IsTrue( rotated.X == 0 );
            Assert.IsTrue( rotated.Y == 1 );
        }

        [TestMethod]
        public void RotateHorizontalVector45DegreesShouldResultInEqualXandYVector()
        {
            //Arrange
            Vector2 original = new Vector2( 1, 0 );

            //Act
            Vector2 rotated = XenMath.Rotate( original, MathHelper.PiOver4 );
            float rotatedAngle = XenMath.GetAngleFloat( rotated );
            float expectedAngle = MathHelper.PiOver4;

            //Assert
            Assert.IsTrue( rotated.X == rotated.Y );
            Assert.IsTrue( rotatedAngle == expectedAngle );
        }

        [TestMethod]
        public void RotateVectorAnyAngleShouldResultInOriginalMagnitude()
        {
            //Arrange
            Vector2 original = new Vector2( 3, 0 );

            //Act
            Vector2 rotated = XenMath.Rotate( original, MathHelper.PiOver4 );

            //Assert
            Assert.IsTrue( XenMath.RoundFloatToFloat( rotated.Length() ) ==
                           XenMath.RoundFloatToFloat( original.Length() ) );
        }

        [TestMethod]
        public void GetAngleForVectorZeroDegreeYieldsZero()
        {
            //Arrange
            Vector2 vector = new Vector2( 1, 0 );

            //Act
            float angle = XenMath.GetAngleFloat( vector );

            //Assert
            Assert.IsTrue( 0.0f == angle );
        }

        [TestMethod]
        public void GetAngleForQuadrantOneVectorYieldsExpectedResult()
        {
            //Arrange
            Vector2 vector = new Vector2( 1, -1 );

            //Act
            float angle = XenMath.GetAngleFloat( vector );
            float expected = XenMath.RoundFloatToFloat( MathHelper.Pi * 7 / 4 );

            //Assert
            AssertHelper.AreApproximatelyEqual( expected, angle );
        }

        [TestMethod]
        public void GetAngleForQuadrantTwoVectorYieldsExpectedResult()
        {
            //Arrange
            Vector2 vector = new Vector2( 1, 1 );

            //Act
            float angle = XenMath.GetAngleFloat( vector );
            float expected = XenMath.RoundFloatToFloat( MathHelper.PiOver4 );

            //Assert
            AssertHelper.AreApproximatelyEqual( expected, angle );
        }

        [TestMethod]
        public void GetAngleForQuadrantThreeVectorYieldsExpectedResult()
        {
            //Arrange
            Vector2 vector = new Vector2( -1, 1 );

            //Act
            float angle = XenMath.GetAngleFloat( vector );
            float expected = XenMath.RoundFloatToFloat( MathHelper.Pi * 3 / 4 );

            //Assert
            AssertHelper.AreApproximatelyEqual( expected, angle );
        }

        [TestMethod]
        public void GetAngleForQuadrantFourVectorYieldsExpectedResult()
        {
            //Arrange
            Vector2 vector = new Vector2( -1, -1 );

            //Act
            float angle = XenMath.GetAngleFloat( vector );
            float expected = XenMath.RoundFloatToFloat( MathHelper.Pi * 5 / 4 );

            //Assert
            AssertHelper.AreApproximatelyEqual( expected, angle );
        }

        [TestMethod]
        public void DifferenceBetweenTwoAnglesIsAsExpected()
        {
            //Arrange
            Vector2 vector1 = new Vector2( -1, 0 ); //180 degrees
            Vector2 vector2 = new Vector2( 0, 1 ); //90 degrees

            //Act
            float angle1 = XenMath.GetAngleFloat( vector1 );
            float angle2 = XenMath.GetAngleFloat( vector2 );

            //Assert
            AssertHelper.AreApproximatelyEqual( MathHelper.Pi / 2, angle1 - angle2 );
        }

        [TestMethod]
        public void CreatingVectorFromAnglesWorks()
        {
            //Arrange
            Vector2 vector1 = XenMath.CreateNormalVectorFromAngleDouble( 0 );
            Vector2 vector2 = XenMath.CreateNormalVectorFromAngleDouble( Math.PI / 2 );
            Vector2 vector3 = XenMath.CreateNormalVectorFromAngleDouble( Math.PI );
            Vector2 vector4 = XenMath.CreateNormalVectorFromAngleDouble( Math.PI * 3 / 2 );

            //Act
            float angle1 = XenMath.GetAngleFloat( vector1 );
            float angle2 = XenMath.GetAngleFloat( vector2 );
            float angle3 = XenMath.GetAngleFloat( vector3 );
            float angle4 = XenMath.GetAngleFloat( vector4 );

            //Assert
            Assert.IsTrue( angle1 == 0 );
            AssertHelper.AreApproximatelyEqual( MathHelper.Pi / 2, angle2 );
            AssertHelper.AreApproximatelyEqual( MathHelper.Pi, angle3 );
            AssertHelper.AreApproximatelyEqual( MathHelper.Pi * 3 / 2, angle4 );
        }

        [TestMethod]
        public void GetDifferenceAngleVectorWorksAsExpected()
        {
            //Arrange
            Vector2 vector1 = new Vector2( 0, 1 ); //angle at 90 degrees
            Vector2 vector2 = new Vector2( -1, 0 ); // angle at 180 degrees

            //Act
            Vector2 angleDiff = XenMath.GetDifferenceAngleVector( vector2, vector1 );

            //Assert
            AssertHelper.AreApproximatelyEqual( MathHelper.Pi / 2, XenMath.GetAngleFloat( angleDiff ) );
        }

        [TestMethod]
        public void ExampleOfWorkingWithMatrix()
        {
            Matrix rotateByNinetyDegrees = Matrix.CreateRotationZ( MathHelper.Pi / 2 );
            Vector2 initial = new Vector2( 1, 0 );
            Vector2 expected = new Vector2( 0, 1 );
            Vector2 result = XenMath.RoundVector( Vector2.Transform( initial, rotateByNinetyDegrees ) );

            Assert.IsTrue( result == expected );
        }

        [TestMethod]
        public void NormalizePositiveAngleYieldsPositive()
        {
            float keyAngle = ( MathHelper.Pi / 4 );
            float originalAngleInRadians = ( 10 * ( 2 * MathHelper.Pi ) ) + keyAngle;
            float outputAngle = XenMath.NormalizeAngleRadians( originalAngleInRadians );

            Assert.IsTrue( XenMath.IsWithinToleranceToTarget( outputAngle, keyAngle, 0.0001f ) );
        }

        [TestMethod]
        public void NormalizeNegativeAngleYieldsPositive()
        {
            float keyAngle = -( MathHelper.Pi / 4 );
            float originalAngleInRadians = ( -10 * ( 2 * MathHelper.Pi ) ) + keyAngle;
            float outputAngle = XenMath.NormalizeAngleRadians( originalAngleInRadians );

            Assert.IsTrue( XenMath.IsWithinToleranceToTarget( MathHelper.TwoPi + keyAngle, outputAngle, 0.0001f ) );
        }

        [TestMethod]
        public void NormalizeSmallAngleYieldsOriginal()
        {
            float keyAngle1 = ( MathHelper.Pi / 2 );
            float keyAngle2 = ( MathHelper.Pi / 4 );
            Assert.AreEqual( XenMath.NormalizeAngleRadians( keyAngle1 ), keyAngle1 );
            Assert.AreEqual( XenMath.NormalizeAngleRadians( keyAngle2 ), keyAngle2 );
        }

        [TestMethod]
        public void ExtentVectorCase1()
        {
            Vector2 result = XenMath.Extend( new Vector2( 1, 1 ), new Vector2( 2, 1 ), 1 );
            Assert.AreEqual( new Vector2( 3, 1 ), result );
        }

        [TestMethod]
        public void ShortestRotationAngleToWorks()
        {
            Assert.AreEqual( 0, XenMath.ShortestRotationAngleTo( 0, 0 ) );
            Assert.AreEqual( 0, XenMath.ShortestRotationAngleTo( 1.5f, 1.5f ) );
            Assert.AreEqual( XenMath.RoundFloatToFloat( 2 * MathHelper.Pi / 4 ), XenMath.ShortestRotationAngleTo( 3 * MathHelper.Pi / 4, MathHelper.Pi / 4 ) );
            Assert.AreEqual( XenMath.RoundFloatToFloat( -2 * MathHelper.Pi / 4 ), XenMath.ShortestRotationAngleTo( 5 * MathHelper.Pi / 4, 7 * MathHelper.Pi / 4 ) );
            Assert.AreEqual( XenMath.RoundFloatToFloat( -2 * MathHelper.Pi / 4 ), XenMath.ShortestRotationAngleTo( 7 * MathHelper.Pi / 4, MathHelper.Pi / 4 ) );
            Assert.AreEqual( XenMath.RoundFloatToFloat( 2 * MathHelper.Pi / 4 ), XenMath.ShortestRotationAngleTo( MathHelper.Pi / 4, 7 * MathHelper.Pi / 4 ) );
        }
    }
}
