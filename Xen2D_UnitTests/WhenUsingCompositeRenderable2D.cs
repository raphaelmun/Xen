using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCompositeRenderable2D
    {
        [TestMethod]
        public void AcquiredInstanceIsVisibleAndEnabled()
        {
            var cr = CompositeRenderable2D.Acquire();
            Assert.IsTrue( cr.Visible );
            Assert.IsTrue( cr.Enabled );
        }
    }
}
