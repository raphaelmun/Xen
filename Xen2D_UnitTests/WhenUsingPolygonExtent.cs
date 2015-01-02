using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingPolygonExtent
    {
        [TestMethod]
        public void ResetWorks()
        {
            PolygonExtent extent = new PolygonExtent();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( -10, -10 ) );
            vertices.Add( new Vector2( 10, -10 ) );
            vertices.Add( new Vector2( 10, 10 ) );
            vertices.Add( new Vector2( -10, 10 ) );

            extent.Reset( vertices );

            Assert.AreEqual( 4, extent.NumSides );
            Assert.AreEqual( -10, extent.LowestX );
            Assert.AreEqual( -10, extent.LowestY );
            Assert.AreEqual( 10, extent.HighestX );
            Assert.AreEqual( 10, extent.HighestY );
        }
    }
}
