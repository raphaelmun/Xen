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
    public class WhenUsingShapePolygon
    {
        [TestMethod]
        public void ResettingVerticesInvokesResetEvent()
        {
            ShapePolygon shape = new ShapePolygon();

            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( 0, 0 ) );
            vertices.Add( new Vector2( 10, 10 ) );
            vertices.Add( new Vector2( 10, 0 ) );

            bool calledBack = false;
            shape.OnReset.Add( () =>
                {
                    calledBack = true;
                } );

            shape.Reset( vertices );

            Assert.IsTrue( calledBack );
        }

        [TestMethod]
        public void InnerRadiusYieldsExpectedResult()
        {
            ShapePolygon shape = new ShapePolygon();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( 0, 0 ) );
            vertices.Add( new Vector2( 20, 0 ) );
            vertices.Add( new Vector2( 20, 10 ) );
            vertices.Add( new Vector2( 0, 10 ) );

            shape.Reset( vertices );

            Assert.AreEqual( 5, shape.InnerRadius );
        }

        [TestMethod]
        public void OuterRadiusYieldsExpectedResult()
        {
            ShapePolygon shape = new ShapePolygon();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( 0, 0 ) );
            vertices.Add( new Vector2( 8, 0 ) );
            vertices.Add( new Vector2( 8, 6 ) );
            vertices.Add( new Vector2( 0, 6 ) );

            shape.Reset( vertices );

            Assert.AreEqual( 5, shape.OuterRadius );
        }
    }
}
