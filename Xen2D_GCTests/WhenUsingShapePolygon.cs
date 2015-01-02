using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingShapePolygon
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            ShapePolygon shape = new ShapePolygon();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( 0, 0 ) );
            vertices.Add( new Vector2( 10, 10 ) );
            vertices.Add( new Vector2( 10, 0 ) );

            HeapUtility.GetHeapStateBefore();
            
            shape.Reset( vertices );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
