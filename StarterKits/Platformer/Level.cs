using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using XenGameBase;

namespace PlatformerXen
{
    public static class Level
    {
        /// <summary>
        /// Loads the specified level into a game scene
        /// </summary>
        /// <param name="level">The level to load (must correspond to a content resouce)</param>
        /// <param name="gameScene">The scene that will contain the level content</param>
        public static void Load( int level, Layer gameScene )
        {
            gameScene.Children.ReleaseAndClearChildren();
            //string levelPath = string.Format( "Content/Levels/{0}.txt", level );
            string levelPath = "Content/Levels/0.txt";
            using( Stream fileStream = TitleContainer.OpenStream( levelPath ) )
            {
                // Load the level and ensure all of the lines are the same length.
                int width;
                List<string> lines = new List<string>();
                using( StreamReader reader = new StreamReader( fileStream ) )
                {
                    string line = reader.ReadLine();
                    width = line.Length;
                    while( line != null )
                    {
                        lines.Add( line );
                        if( line.Length != width )
                            throw new Exception( String.Format( "The length of line {0} is different from all preceeding lines.", lines.Count ) );
                        line = reader.ReadLine();
                    }
                }

                int height = lines.Count;

                for( int y = 0; y < height; y++ )
                {
                    for( int x = 0; x < width; x++ )
                    {
                        TileType tileType = TileFactory.GetTileTypeFromChar( lines[ y ][ x ] );
                        if( tileType == TileType.Platform )
                            gameScene.Children.Add( TileFactory.Create( tileType, x, y ) );
                        else if( tileType == TileType.Gem )
                        {
                            var gem = Gem.Acquire();
                            gem.RenderingExtent.Anchor = new Vector2( 40 * x, 32 * y );
                            gameScene.Children.Add( gem );
                        }
                    }
                }
            }

            gameScene.Children.Add( Player.Acquire() );
            gameScene.Children.Add( Background.Acquire( TexId.Layer2_0, -1 ) );
            gameScene.Children.Add( Background.Acquire( TexId.Layer1_0, -1 ) );
            gameScene.Children.Add( Background.Acquire( TexId.Layer0_0, -1 ) );
        }
    }
}
