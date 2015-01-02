using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingPolygonExtent
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            PolygonExtent extent = new PolygonExtent();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( -10, -10 ) );
            vertices.Add( new Vector2( 10, -10 ) );
            vertices.Add( new Vector2( 10, 10 ) );
            vertices.Add( new Vector2( -10, 10 ) );

            HeapUtility.GetHeapStateBefore();

            extent.Reset( vertices );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void RotationDoesNotGenerateGarbage()
        {
            PolygonExtent extent = new PolygonExtent();
            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( -10, -10 ) );
            vertices.Add( new Vector2( 10, -10 ) );
            vertices.Add( new Vector2( 10, 10 ) );
            vertices.Add( new Vector2( -10, 10 ) );

            HeapUtility.GetHeapStateBefore();

            extent.Angle += MathHelper.PiOver2;

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
