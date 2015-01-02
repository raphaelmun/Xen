using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingText2D
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

        //[TestMethod]
        //public void AcquireWorks()
        //{
        //    // TODO: SpriteFont needs to be added to the mock game
        //    //Text2D.Acquire();
        //    Assert.Inconclusive();
        //}
    }
}
