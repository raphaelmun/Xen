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
    public class WhenInheritingRenderableBase
    {
        class MockRenderable : RenderableBase<MockRenderable>
        {
        }

        [TestMethod]
        public void ResetDoesNotGenerateGarbage()
        {
            MockRenderable mock = new MockRenderable();

            HeapUtility.GetHeapStateBefore();
            mock.Reset();
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
