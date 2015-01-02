using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;
using Microsoft.Xna.Framework;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingAnimationStripDescriptor
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            Texture2D tex = new Texture2D( MockGraphics.GraphicsDevice, 100, 100 );
            AnimationStripDescriptor stripDescriptor = new AnimationStripDescriptor();

            HeapUtility.GetHeapStateBefore();
            stripDescriptor.Reset( tex, 10, 10 );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
