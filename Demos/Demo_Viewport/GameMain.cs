using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_Viewport
{
    public enum Textures : int
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
        Marker_Red
    }

    /// <summary>
    /// This demo shows how to scroll, rotate, and zoom the viewport to display different transforms of content
    /// on a particular screen.  See instructions below:
    /// 
    /// Use W,A,S,D to pan the viewport up, left, down right.  
    /// Use Z, C to zoom the viewport in or out
    /// Use Q and E to rotate the viewport.  
    /// 
    /// A red dot has been added to the center of the camera to track its anchor.  
    /// </summary>
    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;

        List<StaticSprite> _sprites = new List<StaticSprite>();
        StaticSprite _viewportCenter;
        Texture2DCache _textures;
        IViewport2D _viewport = new Viewport2D();
        bool _contentLoaded = false;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        protected override void LoadContent()
        {
            _textures = new Texture2DCache( typeof( Textures ) );
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );

            for( int i = 0; i < 3; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    var sprite = StaticSprite.Acquire( _textures[ (int)Textures.ColoredRect ] );
                    sprite.LayerDepth = 1;
                    sprite.RenderingExtent.Anchor = new Vector2( 100 + i * 250, 100 + j * 250 );
                    _sprites.Add( sprite );
                }
            }

            _viewportCenter = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Red ], new Vector2( 4, 4 ) );
            _viewportCenter.LayerDepth = 0;
            _sprites.Add( _viewportCenter );

            ScreenUtility.InitGraphicsMode( 800, 480, false );

            _viewport.ResetWithScreenSize( 800, 480 );

            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            #region Move Camera
            if( ks.IsKeyDown( Keys.A ) )
            {
                _viewport.MoveHorizontal( -5 );
            }
            if( ks.IsKeyDown( Keys.D ) )
            {
                _viewport.MoveHorizontal( 5 );
            }
            if( ks.IsKeyDown( Keys.W ) )
            {
                _viewport.MoveVertical( -5 );
            }
            if( ks.IsKeyDown( Keys.S ) )
            {
                _viewport.MoveVertical( 5 );
            }
            if( ks.IsKeyDown( Keys.Q ) )
            {
                _viewport.RotateBy( -0.04f );
            }
            if( ks.IsKeyDown( Keys.E ) )
            {
                _viewport.RotateBy( 0.04f );
            }
            if( ks.IsKeyDown( Keys.Z ) )
            {
                _viewport.ZoomIn( 1.04f );
            }
            if( ks.IsKeyDown( Keys.C ) )
            {
                _viewport.ZoomOut( 1.04f );
            }
            #endregion

            if( _contentLoaded )
            {
                _viewportCenter.RenderingExtent.Anchor = _viewport.Anchor;
            }
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            Matrix worldToCamera = _viewport.Transform.TranslateTo;

            _spriteBatch.Begin();

            if( _contentLoaded )
            {
                foreach( var sprite in _sprites )
                {
                    _spriteBatch.DrawSprite( sprite, worldToCamera, RenderParams.Debug );
                }
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
