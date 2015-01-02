using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenGameBase;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenUsingGameBase
    {
        class MockGame : GameBase 
        { 
            public bool AccessCanPause
            {
                get { return CanPause; }
                set { CanPause = value; }
            }
        }

        [TestMethod]
        public void NewGameCanBePaused()
        {
            var mock = new MockGame();
            Assert.IsTrue( mock.CanPause );
        }

        [TestMethod]
        public void NewGameIsNotPaused()
        {
            var mock = new MockGame();
            Assert.IsFalse( mock.IsPaused );
        }

        [TestMethod]
        public void PausedGetBecomesUnpausedIfCanPauseIsSetToFalse()
        {
            var mock = new MockGame();
            mock.IsPaused = true;
            Assert.IsTrue( mock.IsPaused );

            mock.AccessCanPause = false;

            Assert.IsFalse( mock.IsPaused );
        }
    }
}
