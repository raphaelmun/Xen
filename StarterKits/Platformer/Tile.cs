using XenGameBase;
using Xen2D;
using System;
using Microsoft.Xna.Framework;

namespace PlatformerXen
{
    public enum TileType
    {
        None,
        Gem,
        Platform
    }

    public static class TileFactory
    {
        public static TileType GetTileTypeFromChar( char c )
        {
            switch( c )
            {
                case '.':
                case '1':
                case 'X':
                    return TileType.None;
                case 'G':
                    return TileType.Gem;
                case '#':
                    return TileType.Platform;
                default:
                    throw new NotSupportedException( "unrecognized tile character encoding" );
            }
        }

        public static Tile Create( TileType type, int x, int y )
        {
            Tile tile = null;
            switch( type )
            {
                case TileType.Platform:
                    tile = Tile.Acquire( TexId.BlockB0, (uint)PlatformerCollisionClass.Tile_Platform );
                    break;
                default:
                    throw new NotSupportedException( "unsupported tile type" );
            }

            //Position the tile based on the specific x, y
            tile.RenderingExtent.Anchor += new Vector2( x * tile.RenderingExtent.ReferenceWidth, y * tile.RenderingExtent.ReferenceHeight );
            return tile;
        }
    }

    public class Tile : Element2D<Tile>
    {
        public static Tile Acquire( TexId texId, uint collisionClass )
        {
            var instance = _pool.Acquire();
            instance.Reset( texId );
            instance.CollisionClass = collisionClass;
            return instance;
        }

        StaticSprite _tileSprite;

        public void Reset( TexId texId )
        {
            VisualComponent = StaticSprite.Acquire( Textures.Get( texId ) );
            base.Reset();
        }
    }
}
