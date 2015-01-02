using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingSinglePixel
    {
        InjectableDummyGame game = null;

        [TestInitialize()]
        public void TestInitialize()
        {
            game = new InjectableDummyGame();
            Globals.Graphics = game.graphics;
            Globals.Content = game.Content;
        }

        [TestCleanup()]
        public void TestUninitialize()
        {
            game = null;
        }
    }
}
