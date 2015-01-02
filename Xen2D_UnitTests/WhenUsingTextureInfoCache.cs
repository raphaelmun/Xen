using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingTextureInfoCache
    {
        enum SampleTextures
        {
            [TextureSize( 1, 1 )]
            Tex1,
            [TextureSize( 2, 2 )]
            Tex2,
        }

        [TestMethod]
        public void SimpleEnumerationReturnsDecoratedSizes()
        {
            TextureInfoCache cache = new TextureInfoCache( typeof( SampleTextures ) );
            Assert.AreEqual( new Vector2( 1, 1 ), cache[ (int)SampleTextures.Tex1 ] );
            Assert.AreEqual( new Vector2( 2, 2 ), cache[ (int)SampleTextures.Tex2 ] );
        }
    }
}
