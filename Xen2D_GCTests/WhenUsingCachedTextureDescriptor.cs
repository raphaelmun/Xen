using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingCachedTextureDescriptor
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            Texture2D tex = new Texture2D( MockGraphics.GraphicsDevice, 100, 100 );
            CachedTextureDescriptor cached = new CachedTextureDescriptor();

            HeapUtility.GetHeapStateBefore();
            cached.Reset( tex );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
