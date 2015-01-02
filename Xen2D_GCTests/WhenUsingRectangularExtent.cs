using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects_UnitTests;
using System.Collections.Generic;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingRectangularExtent
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            RectangularExtent extent = new RectangularExtent();

            HeapUtility.GetHeapStateBefore();
            extent.Reset( new Vector2( 50, 100 ), new Vector2( 50, 50 ) );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}