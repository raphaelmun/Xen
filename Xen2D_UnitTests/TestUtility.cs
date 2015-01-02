using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Xen2D_UnitTests
{
    internal static class TestUtility
    {
        internal static bool AreTextureValuesEqual( Texture2D expected, Texture2D actual )
        {
            // Check for width, height, format equivalence
            if( expected.Width != actual.Width ||
                expected.Height != actual.Height ||
                expected.Format != actual.Format )
            {
                return false;
            }

            // Check at the pixel level
            Color[] expectedTextureData = new Color[ expected.Width * expected.Height ];
            expected.GetData<Color>( expectedTextureData );
            Color[] actualTextureData = new Color[ actual.Width * actual.Height ];
            actual.GetData<Color>( actualTextureData );

            for( int y = 0; y < expected.Height; y++ )
            {
                for( int x = 0; x < expected.Width; x++ )
                {
                    if( expectedTextureData[ y * expected.Width + x ] != actualTextureData[ y * actual.Width + x ] )
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
