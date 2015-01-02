using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingVectorUtility
    {
        [TestMethod]
        public void ZeroDoesNotGenerateGarbage()
        {
            //Have to call a method on VectorUtility first so that the static instance gets loaded into memory
            Vector2 vec = VectorUtility.Zero;
            HeapUtility.GetHeapStateBefore();
            vec = VectorUtility.Zero;
            HeapState delta = HeapUtility.GetHeapStateDelta();

            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
