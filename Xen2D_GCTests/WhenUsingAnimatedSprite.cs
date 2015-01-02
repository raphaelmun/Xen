using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingAnimatedSprite
    {
        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            Texture2D strip = new Texture2D( MockGraphics.GraphicsDevice, 100, 100 );
            HeapUtility.GetHeapStateBefore();
            sprite.Reset( strip, 10, 10, new Vector2( 100, 100 ), VectorUtility.Zero );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
