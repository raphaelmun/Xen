using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Xen2D;
using Microsoft.Xna.Framework.Graphics;

namespace Xen2D_UnitTests
{
    internal static class MockGraphics
    {
        static InjectableDummyGame _mockGame = new InjectableDummyGame();

        public static GraphicsDeviceManager Graphics
        {
            get { return _mockGame.graphics; }
        }

        public static GraphicsDevice GraphicsDevice
        {
            get { return _mockGame.graphics.GraphicsDevice; }
        }

        public static ContentManager Content
        {
            get { return _mockGame.Content; }
        }
    }
}
