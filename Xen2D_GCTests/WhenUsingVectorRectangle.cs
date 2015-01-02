using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingVectorRectangle
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            VectorRectangle vr = VectorRectangle.Acquire( 100, 100 );

            HeapUtility.GetHeapStateBefore();
            vr.Reset( 200, 200 );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
