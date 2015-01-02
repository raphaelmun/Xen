using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_CollisionSimple
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\cursor" )]
        Cursor,
        [ContentIdentifier( "textures\\simple_square_64x64" )]
        Square,
        [ContentIdentifier( "textures\\gray_rect_100x200" )]
        GrayRect,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    class Textures : Texture2DCache<TexId>
    {
        public static readonly Textures Instance = new Textures();

        public static Texture2D Get( TexId tex )
        {
            return Instance[ tex ];
        }

        public override Texture2D this[ TexId tex ]
        {
            get { return this[ (int)tex ]; }
        }
    }
    #endregion

    /// <summary>
    /// This demo shows how to listen on the collision event for a layer and handle collisions
    /// </summary>
    public class GameMain : GameBase
    {
        protected override void LoadContent()
        {
            MainScene.Children.Add( CollidableElement.Acquire( new Vector2( 200, 200 ), 0.03f ) );
            MainScene.Children.Add( CollidableElement.Acquire( new Vector2( 400, 200 ), -0.03f ) );
            base.LoadContent();
        }

        protected override void OnMainSceneCollisionHandler( CollisionEventArgs collisionEvent )
        {
            collisionEvent.A.Release();
            collisionEvent.B.Release();
        }
    }
}
