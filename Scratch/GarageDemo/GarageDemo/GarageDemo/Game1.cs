using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#if SURFACE
using Microsoft.Surface;
using Microsoft.Surface.Core;
#endif
using XenGameBase;
using Xen2D;

namespace GarageDemo
{
    #region Textures
    public enum TexId : int
    {
        [ContentIdentifier( "textures\\colored_rect_200x200" )]
        ColoredRect,
        [ContentIdentifier( "textures\\explosion0" )]
        Explosion,
        [ContentIdentifier( "textures\\marker_blue" )]
        Marker_Blue,
        [ContentIdentifier( "textures\\marker_green" )]
        Marker_Green,
        [ContentIdentifier( "textures\\marker_red" )]
        Marker_Red,
        [ContentIdentifier( "textures\\gray_rect_100x200" )]
        GrayRect,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    public class Textures : Texture2DCache<TexId>
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

    #region Fonts
    public enum FontId : int
    {
        [ContentIdentifier( "Arial" )]
        Arial,
        [ContentIdentifier( "CourierNew" )]
        CourierNew,
        [ContentIdentifier( "TimesNewRoman" )]
        TimesNewRoman,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    public class Fonts : SpriteFontCache<FontId>
    {
        public static readonly Fonts Instance = new Fonts();

        public static SpriteFont Get( FontId tex )
        {
            return Instance[ tex ];
        }

        public override SpriteFont this[ FontId tex ]
        {
            get { return this[ (int)tex ]; }
        }
    }
    #endregion

    /// <summary>
    /// This is the main type for your application.
    /// </summary>
#if SURFACE
    public class Game1 : SurfaceGameBase
#else
    public class Game1 : GameBase
#endif
    {
        Layer _demoUISelectionLayer;
        Layer _complexElementLayer;
        Layer _complexPolygonLayer;
        Layer _animatedSpriteLayer;
        Layer _tweensLayer;

        public Game1()
        {
            //BackgroundColor = Color.CadetBlue;
            BackgroundColor = new Color( 81, 81, 81 );
        }

        protected override void LoadContent()
        {
            _complexElementLayer = Layer.Acquire();
            _complexElementLayer.Children.Add( ComplexElement.Acquire() );
            _complexElementLayer.DrawOrder = 5;

            _complexPolygonLayer = Layer.Acquire();
            _complexPolygonLayer.Children.Add( ComplexPolygonElement.Acquire() );
            _complexPolygonLayer.DrawOrder = 10;
            _complexPolygonLayer.Enabled = false;
            _complexPolygonLayer.Visible = false;

            _animatedSpriteLayer = Layer.Acquire();
            _animatedSpriteLayer.Children.Add( AnimatedSpriteElement.Acquire() );
            _animatedSpriteLayer.DrawOrder = 15;
            _animatedSpriteLayer.Enabled = false;
            _animatedSpriteLayer.Visible = false;

            _tweensLayer = Layer.Acquire();
            _tweensLayer.Children.Add( TweensElement.Acquire() );
            _tweensLayer.DrawOrder = 20;
            _tweensLayer.Enabled = false;
            _tweensLayer.Visible = false;


            _demoUISelectionLayer = Layer.Acquire();
            _demoUISelectionLayer.Children.Add( LayerToggleTextButtonElement.Acquire( new Vector2( 50, 50 ), "Complex Element Demo", _complexElementLayer ) );
            _demoUISelectionLayer.Children.Add( LayerToggleTextButtonElement.Acquire( new Vector2( 50, 100 ), "Complex Polygon Demo", _complexPolygonLayer ) );
            _demoUISelectionLayer.Children.Add( LayerToggleTextButtonElement.Acquire( new Vector2( 50, 150 ), "Animated Sprite Demo", _animatedSpriteLayer ) );
            _demoUISelectionLayer.Children.Add( LayerToggleTextButtonElement.Acquire( new Vector2( 50, 200 ), "Tweens Demo", _tweensLayer ) );

            _demoUISelectionLayer.DrawOrder = 100;

            MainScene.Children.Add( _demoUISelectionLayer );
            MainScene.Children.Add( _complexElementLayer );
            MainScene.Children.Add( _complexPolygonLayer );
            MainScene.Children.Add( _animatedSpriteLayer );
            MainScene.Children.Add( _tweensLayer );
        }

        protected override void Update( GameTime gameTime )
        {
#if SURFACE
            if( isApplicationActivated || isApplicationPreviewed )
            {
                if( isApplicationActivated )
                {
#endif
                    // TODO: Process contacts
                    // Use the following code to get the state of all current contacts.
                    // ReadOnlyContactCollection contacts = contactTarget.GetState();
#if SURFACE
                }

                // TODO: Add your update logic here
            }
#endif

            base.Update( gameTime );
        }

        protected override void ProcessInput( ref InputState input )
        {
            base.ProcessInput( ref input );
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                input.IsKeyPressed( Keys.Escape ) )
                this.Exit();
        }
    }
}
