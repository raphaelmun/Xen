using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenFarseer;
using XenGameBase;

namespace Demo_FarseerPlugin
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
    
    #region Sound Effects
    enum SfxId : int
    {
        [ContentIdentifier( "sfx\\coin_pickup" )]
        CoinPickup,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    class SoundEffects : SoundEffectCache<SfxId>
    {
        public static readonly SoundEffects Instance = new SoundEffects();

        public static SoundEffect Get( SfxId sfx )
        {
            return Instance[ sfx ];
        }

        public override SoundEffect this[ SfxId sfx ]
        {
            get { return this[ (int)sfx ]; }
        }
    }
    #endregion

    /// <summary>
    /// This demo shows how to listen on the collision event for a layer and handle collisions
    /// </summary>
    public class GameMain : GameBase
    {
        protected override void Initialize()
        {
            PluginDirectory = Directory.GetCurrentDirectory();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            MainScene.Children.Add( CollidableElement.Acquire( new Vector2( 400, 200 ) ) );
            MainScene.Children.Add( CollidableElement.Acquire( new Vector2( 100, 100 ) ) );

            //MouseScrollZoomBehavior.AcquireAndAttach( MainScene );
            //MainScene.Viewport.ZoomOut( 5 );

            //CallPluginFunction( "XenFarseer", (int)XenFarseerFunc.SetGravity, Vector2.UnitY * 9.81f );

            XenFarseer_DynamicObjectBehavior.AcquireAndAttach( MainScene.Children[ 0 ] );
            XenFarseer_DynamicObjectBehavior.AcquireAndAttach( MainScene.Children[ 1 ] );

            XenFarseer_RotationBehavior.AcquireAndAttach( MainScene.Children[ 0 ], 5.0f );
            XenFarseer_RotationBehavior.AcquireAndAttach( MainScene.Children[ 1 ], -1.0f );

            XenFarseer_InitialMovementBehavior.AcquireAndAttach( MainScene.Children[ 0 ], Vector2.UnitX * 100.0f );

            IElement2D controller = SquareElement.Acquire( new Vector2( 50, 50 ) );
            controller.RenderingExtent.Scale = Vector2.One * 0.5f;
            //XenFarseer_ChaseCursorBehavior.AcquireAndAttach( controller );
            XenFarseer_KeyboardControlBehavior.AcquireAndAttach( controller );
            MainScene.Children.Add( controller );
            base.LoadContent();
        }

        protected override void OnMainSceneCollisionHandler( CollisionEventArgs collisionEvent )
        {
            if( collisionEvent.A.GetType() == typeof( CollidableElement ) &&
                collisionEvent.B.GetType() == typeof( CollidableElement ) )
            {
                SoundEffects.Instance[ SfxId.CoinPickup ].Play( 0.1f, 0, 0 );
            }
        }
    }
}
