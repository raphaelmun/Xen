using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenConstructingAnimationStrips
    {
        [TestMethod]
        public void FrameSizeLargerThanStripSizeShouldReturnStripSize()
        {
            //Arrange
            Rectangle strip = new Rectangle( 0, 0, 10, 10 );
            Rectangle frameSize = new Rectangle( 0, 0, 20, 20 );

            //Act
            Rectangle[] frames = AnimationStripDescriptor.GetFrameRects( strip, frameSize );

            //Assert
            Assert.IsTrue( 1 == frames.Length );
            Assert.AreEqual( strip, frames[ 0 ] );
        }

        [TestMethod]
        public void StripSizeAndFrameOfSameSizeShouldReturnFramesOfLength1()
        {
            //Arrange
            Rectangle strip = new Rectangle( 0, 0, 10, 10 );
            Rectangle frameSize = new Rectangle( 0, 0, 10, 10 );

            //Act
            Rectangle[] frames = AnimationStripDescriptor.GetFrameRects( strip, frameSize );

            //Assert
            Assert.IsTrue( 1 == frames.Length );
        }

        [TestMethod]
        public void StripSizeSlightlyLargerThanFrameSizeShouldReturnFramesOfLength1()
        {
            //Arrange
            Rectangle strip = new Rectangle( 0, 0, 11, 11 );
            Rectangle frameSize = new Rectangle( 0, 0, 10, 10 );

            //Act
            Rectangle[] frames = AnimationStripDescriptor.GetFrameRects( strip, frameSize );

            //Assert
            Assert.IsTrue( 1 == frames.Length );
        }

        [TestMethod]
        public void StripSizeWithEvenlyDivisibleFramesShouldContainExpectedFrameCount()
        {
            //Arrange
            Rectangle strip = new Rectangle( 0, 0, 100, 100 );
            Rectangle frameSize = new Rectangle( 0, 0, 10, 10 );

            //Act
            Rectangle[] frames = AnimationStripDescriptor.GetFrameRects( strip, frameSize );

            //Assert
            Assert.IsTrue( 100 == frames.Length );
        }

        [TestMethod]
        public void StripSizeWithFourFramesShouldContainExactFrameRects()
        {
            //Arrange
            Rectangle strip = new Rectangle( 0, 0, 20, 20 );
            Rectangle frameSize = new Rectangle( 0, 0, 10, 10 );

            //Act
            Rectangle[] frames = AnimationStripDescriptor.GetFrameRects( strip, frameSize );

            //Assert
            Assert.AreEqual( new Rectangle( 0, 0, 10, 10 ), frames[ 0 ] );
            Assert.AreEqual( new Rectangle( 10, 0, 10, 10 ), frames[ 1 ] );
            Assert.AreEqual( new Rectangle( 0, 10, 10, 10 ), frames[ 2 ] );
            Assert.AreEqual( new Rectangle( 10, 10, 10, 10 ), frames[ 3 ] );
        }
    }
}
