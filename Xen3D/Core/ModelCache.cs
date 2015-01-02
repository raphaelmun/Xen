using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen3D
{
    //Example TextureEnumeration definition
    //public enum Models : int
    //{
    //    [ContentIdentifier( "models\\tank" )]
    //    Tank,
    //    [ContentIdentifier( "models\\car" )]
    //    Car,
    //    [ContentIdentifier( "models\\house" )]
    //    House
    //}

    /// <summary>
    /// This class contains a set of models.  It assumes that model data is static and will never change.
    /// The ModelCache should be constructed and initialized when the game/level is loaded.
    /// Model enumeration definitions must inherit from integer and provide ContentIdentifierAttributes
    /// </summary>
    public class ModelCache : XenCache<Model>
    {
        // Model Cache
        public ModelCache( Type contentElementEnumeration ) : this( Globals.Content, contentElementEnumeration ) { }
        public ModelCache( ContentManager content, Type contentElementEnumeration ) : base( content, contentElementEnumeration ) { }
    }

    public abstract class ModelCache<T> : XenCache<Model, T>
    {
        public ModelCache() : base( typeof( T ) ) { }
    }
}