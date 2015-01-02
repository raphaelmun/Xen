using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingShapeCollision
    {
        Vector2 circleA_center = Vector2.Zero;
        const float circleA_radius = 5.0f;
        Vector2 circleB_center = Vector2.UnitX * 5.0f;
        const float circleB_radius = 10.0f;
        Vector2 circleC_center = Vector2.UnitX * 20.0f;
        const float circleC_radius = 5.0f;
        Vector2 circleD_center = Vector2.UnitX * 10.0f;
        const float circleD_radius = 5.0f;
        Vector2 circleE_center = Vector2.Zero;
        const float circleE_radius = 2.5f;

        static List<Vector2> singlePoint = new List<Vector2>();
        static List<Vector2> edge = new List<Vector2>();
        static List<Vector2> triangleA = new List<Vector2>();
        static List<Vector2> triangleB = new List<Vector2>();
        static List<Vector2> triangleC = new List<Vector2>();
        static List<Vector2> triangleD = new List<Vector2>();
        static List<Vector2> triangleE = new List<Vector2>();

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void InitializeCollisionTestsClass( TestContext testContext )
        {
            singlePoint.Add( Vector2.Zero );
            edge.Add( Vector2.Zero );
            edge.Add( Vector2.One );

            // Triangle-A: (0,0), (0,1), (1,0)
            triangleA.Add( Vector2.Zero );
            triangleA.Add( Vector2.UnitY );
            triangleA.Add( Vector2.UnitX );

            // Triangle-B: (-1,-1), (0.5,0.5), (-1,0)
            triangleB.Add( -Vector2.One );
            triangleB.Add( Vector2.One / 2.0f );
            triangleB.Add( -Vector2.UnitX );

            // Triangle-C: (2,2), (0,5), (-2,3)
            triangleC.Add( Vector2.One * 2 );
            triangleC.Add( Vector2.UnitY * 5 );
            triangleC.Add( new Vector2( -2, 3 ) );

            // Triangle-D: (0,0), (0,-1), (-1,0)
            triangleD.Add( Vector2.Zero );
            triangleD.Add( -Vector2.UnitY );
            triangleD.Add( -Vector2.UnitX );

            // Triangle-E: (0.1,0.1), (0.1,-0.9), (-0.9,0.1)
            triangleE.Add( new Vector2( 0.1f, 0.1f ) );
            triangleE.Add( new Vector2( 0.1f, 0.9f ) );
            triangleE.Add( new Vector2( 0.9f, 0.1f ) );
        }

        private static Vector2 _CalculateCenterPoint( List<Vector2> pointList )
        {
            Vector2 pointSum = Vector2.Zero;
            foreach( Vector2 point in pointList )
            {
                pointSum += point;
            }
            Vector2 pointAverage = pointSum / pointList.Count;
            return pointAverage;
        }

        #region Instancing Tests

        [TestMethod]
        public void CreateCircleWorks()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
        }

        [TestMethod]
        public void CreateTriangleWorks()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
        }

        [TestMethod]
        public void Create1PointPolygonFails()
        {
            try
            {
                ShapePolygon onePointPolygon = ShapePolygon.Acquire( singlePoint );
                Assert.Fail();
            }
            catch( ArgumentException )
            {
                Assert.IsTrue( true );
            }
        }

        [TestMethod]
        public void Create2PointPolygonFails()
        {
            try
            {
                ShapePolygon twoPointPolygon = ShapePolygon.Acquire( edge );
                Assert.Fail();
            }
            catch( ArgumentException )
            {
                Assert.IsTrue( true );
            }
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void CircleCenterPropertyIsCorrect()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.AreEqual( circle.Center, circleA_center );
        }

        [TestMethod]
        public void CircleRadiusPropertyIsCorrect()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.AreEqual( circle.Radius, circleA_radius );
        }

        [TestMethod]
        public void TriangleCenterPropertyIsCorrect()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            // Average of the triangle points
            Vector2 pointAverage = _CalculateCenterPoint( triangleA );
            Assert.AreEqual( pointAverage, triangle.Center );
        }

        #endregion

        #region Point Tests

        [TestMethod]
        public void CircleContainsPointInsideCircle()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsTrue( circle.Contains( circleA_center ) );
        }

        [TestMethod]
        public void CircleDoesNotContainPointOutsideCircle()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsFalse( circle.Contains( circleC_center ) );
        }

        [TestMethod]
        public void CircleContainsPointTangentToCircle()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsTrue( circle.Contains( circleB_center ) );
        }

        [TestMethod]
        public void TriangleContainsPointInTriangle()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            Assert.IsTrue( triangle.Contains( triangle.Center ) );
        }

        [TestMethod]
        public void TriangleDoesNotContainPointOutsideTriangle()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            Assert.IsFalse( triangle.Contains( triangleC[ 0 ] ) );
        }

        [TestMethod]
        public void TriangleContainsVertexOfTriangle()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            Assert.IsTrue( triangle.Contains( triangleA[ 0 ] ) );
        }

        #endregion

        #region Collision Tests

        [TestMethod]
        public void CircleIntersectsWithTheSameLogicalCircle()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsTrue( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void CircleIntersectsWithCollidingCircle()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleB_center, circleB_radius );
            Assert.IsTrue( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void CircleDoesNotIntersectWithNonCollidingCircle()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleC_center, circleC_radius );
            Assert.IsFalse( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void CircleIntersectsWithTangentialCircle()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleD_center, circleD_radius );
            Assert.IsTrue( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void CircleIntersectsWithCircleContainedInside()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleE_center, circleE_radius );
            Assert.IsTrue( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void CircleIntersectsWhenContainedInOtherCircle()
        {
            ShapeCircle circle1 = ShapeCircle.Acquire( circleE_center, circleE_radius );
            ShapeCircle circle2 = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsTrue( circle1.Intersects( circle2 ) );
        }

        [TestMethod]
        public void TriangleIntersectsWithSameLogicalTriangle()
        {
            ShapePolygon triangle1 = ShapePolygon.Acquire( triangleA );
            ShapePolygon triangle2 = ShapePolygon.Acquire( triangleA );
            Assert.IsTrue( triangle1.Intersects( triangle2 ) );
        }

        [TestMethod]
        public void TriangleIntersectsWithCollidingTriangle()
        {
            ShapePolygon triangle1 = ShapePolygon.Acquire( triangleA );
            ShapePolygon triangle2 = ShapePolygon.Acquire( triangleB );
            Assert.IsTrue( triangle1.Intersects( triangle2 ) );
        }

        //TODO: fix this unit test.
        //[TestMethod]
        //public void TriangleDoesNotIntersectWithNonCollidingTriangle()
        //{
        //    ShapePolygon triangle1 = ShapePolygon.Acquire( triangleA );
        //    ShapePolygon triangle2 = ShapePolygon.Acquire( triangleC );
        //    Assert.IsFalse( triangle1.Intersects( triangle2 ) );
        //}

        [TestMethod]
        public void TriangleIntersectsWithTangentialTriangle()
        {
            ShapePolygon triangle1 = ShapePolygon.Acquire( triangleA );
            ShapePolygon triangle2 = ShapePolygon.Acquire( triangleD );
            Assert.IsTrue( triangle1.Intersects( triangle2 ) );
        }

        [TestMethod]
        public void TriangleIntersectsWithTriangleContainedInside()
        {
            ShapePolygon triangle1 = ShapePolygon.Acquire( triangleA );
            ShapePolygon triangle2 = ShapePolygon.Acquire( triangleE );
            Assert.IsTrue( triangle1.Intersects( triangle2 ) );
        }

        [TestMethod]
        public void TriangleIntersectsWhenContainedInsideOtherTriangle()
        {
            ShapePolygon triangle1 = ShapePolygon.Acquire( triangleE );
            ShapePolygon triangle2 = ShapePolygon.Acquire( triangleA );
            Assert.IsTrue( triangle1.Intersects( triangle2 ) );
        }

        [TestMethod]
        public void CircleIntersectsWithTriangle()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            Assert.IsTrue( circle.Intersects( triangle ) );
        }

        [TestMethod]
        public void TriangleIntersectsWithCircle()
        {
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            ShapeCircle circle = ShapeCircle.Acquire( circleA_center, circleA_radius );
            Assert.IsTrue( triangle.Intersects( circle ) );
        }

        [TestMethod]
        public void CircleDoesNotIntersectWithNonCollidingTriangle()
        {
            ShapeCircle circle = ShapeCircle.Acquire( circleD_center, circleD_radius );
            ShapePolygon triangle = ShapePolygon.Acquire( triangleA );
            Assert.IsFalse( circle.Intersects( triangle ) );
        }

        #endregion
    }
}
