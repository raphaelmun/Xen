using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingExtentHelper
    {
        [TestMethod]
        public void CalculatingGetCenterToCenterDistanceYieldsCorrectResult()
        {
            RectangularExtent extent1 = new RectangularExtent();
            extent1.Reset( 0, 0, 1, 1 );
            RectangularExtent extent2 = new RectangularExtent();
            extent2.Reset( 3, 4, 1, 1 );
            Assert.AreEqual( 5, ExtentHelper.GetCenterToCenterDistance( extent1, extent2 ) );
        }
    }
}
