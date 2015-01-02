using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenInheritingRenderableBase
    {
        class MockRenderable : RenderableBase<MockRenderable>
        {
        }

        [TestMethod]
        public void DerivedInstanceIsVisibleByDefault()
        {
            MockRenderable mock = MockRenderable.Acquire();
            Assert.IsTrue( mock.Visible );
        }

        [TestMethod]
        public void ResetRestoresVisibleToTrue()
        {
            MockRenderable mock = MockRenderable.Acquire();
            mock.Visible = false;
            mock.Reset();
            Assert.IsTrue( mock.Visible );
        }

        [TestMethod]
        public void ResetRestoresEnabledToTrue()
        {
            MockRenderable mock = MockRenderable.Acquire();
            mock.Enabled = false;
            mock.Reset();
            Assert.IsTrue( mock.Enabled );
        }

        [TestMethod]
        public void ResetRestoresUpdateOrderToDefault()
        {
            MockRenderable mock = MockRenderable.Acquire();
            mock.UpdateOrder = 23847;
            mock.Reset();
            Assert.AreEqual( MockRenderable.DefaultUpdateOrder, mock.UpdateOrder );
        }
    }
}
