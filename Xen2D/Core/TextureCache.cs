using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Xen2D
{
    //Example TextureEnumeration definition
    //public enum Textures : int
    //{
    //    [ContentIdentifier( "textures\\colored_rect_200x200" )]
    //    ColoredRect,
    //    [ContentIdentifier( "textures\\explosion0" )]
    //    Explosion,
    //    [ContentIdentifier( "textures\\marker_blue" )]
    //    Marker_Blue,
    //    [ContentIdentifier( "textures\\marker_green" )]
    //    Marker_Green,
    //    [ContentIdentifier( "textures\\marker_red" )]
    //    Marker_Red
    //}

    /// <summary>
    /// This class contains a set of textures.  It assumes that texture data is static and will never change.
    /// The Texture2DCache should be constructed and initialized when the game/level is loaded.
    /// Texture enumeration definitions must inherit from integer and provide ContentIdentifierAttributes
    /// </summary>
    public class Texture2DCache : XenCache<Texture2D>
    {
        // Texture2D Cache
        public Texture2DCache( Type contentElementEnumeration ) : this( Globals.Content, contentElementEnumeration ) { }
        public Texture2DCache( ContentManager content, Type contentElementEnumeration ) : base( content, contentElementEnumeration ) { }
    }

    public abstract class Texture2DCache<T> : XenCache<Texture2D, T>
    {
        public Texture2DCache() : base( typeof ( T ) ) { }
    }
    
#if !SILVERLIGHT
    /// <summary>
    /// This class contains a set of 3D textures.  It assumes that texture data is static and will never change.
    /// The Texture3DCache should be constructed and initialized when the game/level is loaded.
    /// Texture enumeration definitions must inherit from integer and provide ContentIdentifierAttributes
    /// </summary>
    public class Texture3DCache : XenCache<Texture3D>
    {
        // Texture3D Cache
        public Texture3DCache( Type contentElementEnumeration ) : this( Globals.Content, contentElementEnumeration ) { }
        public Texture3DCache( ContentManager content, Type contentElementEnumeration ) : base( content, contentElementEnumeration ) { }
    }

    public abstract class Texture3DCache<T> : XenCache<Texture3D, T>
    {
        public Texture3DCache() : base( typeof( T ) ) { }
    }
#endif
}