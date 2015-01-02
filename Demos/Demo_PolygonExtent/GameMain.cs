using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace Demo_PolygonExtent
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
        Marker_Red,
        [ContentIdentifier( "textures\\gray_rect_100x200" )]
        GrayRect,
    }

    /// <summary>
    /// This demo shows how a polygon's extent can be drawn and transformed.
    /// 
    /// Use W,A,S,D to pan the viewport up, left, down right.  
    /// Use Z, C to zoom the viewport in or out
    /// Use Q and E to rotate the viewport.  
    /// </summary>
    public class GameMain : Game
    {
        Texture2DCache _textures;
        SpriteBatch _spriteBatch;
        RenderParams _renderParamsTemplate = RenderParams.Debug;
        KeyboardState _previousKeyboardState;

        StaticSprite _markerAnchor_Blue;
        StaticSprite _markerTopLeft_Red;
        StaticSprite _markerCompositeAnchor_Green;
        IPolygonExtent _polygon;

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

            _markerAnchor_Blue = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Blue ], new Vector2( 4, 4 ) );
            _markerAnchor_Blue.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkCenter = new Getter<ISprite>( () => { return _markerAnchor_Blue; } );

            _markerTopLeft_Red = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Red ], new Vector2( 4, 4 ) );
            _markerTopLeft_Red.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkOrigin = new Getter<ISprite>( () => { return _markerTopLeft_Red; } );

            _markerCompositeAnchor_Green = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Green ], new Vector2( 4, 4 ) );
            _markerCompositeAnchor_Green.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkTopLeft = new Getter<ISprite>( () => { return _markerCompositeAnchor_Green; } );

            List<Vector2> vertices = new List<Vector2>();
            vertices.Add( new Vector2( 100, 100 ) );
            vertices.Add( new Vector2( 150, 50 ) );
            vertices.Add( new Vector2( 200, 100 ) );
            vertices.Add( new Vector2( 120, 120 ) );

            _polygon = new PolygonExtent();
            _polygon.Reset( vertices );
            _polygon.ReAnchor( _polygon.ActualCenter );

            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            if( ks.IsKeyDown( Keys.Escape ) )
            {
                this.Exit();
            }
            else
            {
                #region Apply changes to the extent.
                if( ks.IsKeyDown( Keys.A ) )
                {
                    _polygon.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    _polygon.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    _polygon.Anchor += new Vector2( 0, -5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    _polygon.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    _polygon.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    _polygon.Angle += 0.04f;
                }

                if( ks.IsKeyDown( Keys.Z ) )
                {
                    _polygon.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    _polygon.Scale /= 1.04f;
                }

                if( ks.IsKeyDown( Keys.F ) )
                {
                    _polygon.Scale = new Vector2( _polygon.Scale.X, _polygon.Scale.Y * 1.04f );
                }
                if( ks.IsKeyDown( Keys.G ) )
                {
                    _polygon.Scale = new Vector2( _polygon.Scale.X, _polygon.Scale.Y / 1.04f );
                }
                if( ks.IsKeyDown( Keys.V ) )
                {
                    _polygon.Scale = new Vector2( _polygon.Scale.X * 1.04f, _polygon.Scale.Y );
                }
                if( ks.IsKeyDown( Keys.B ) )
                {
                    _polygon.Scale = new Vector2( _polygon.Scale.X / 1.04f, _polygon.Scale.Y );
                }
                #endregion
            }

            if( _previousKeyboardState.IsKeyDown( Keys.D1 ) && ks.IsKeyUp( Keys.D1 ) )
                _renderParamsTemplate.Mode ^= RenderMode.Texture;
            if( _previousKeyboardState.IsKeyDown( Keys.D2 ) && ks.IsKeyUp( Keys.D2 ) )
                _renderParamsTemplate.Mode ^= RenderMode.TraceBoundingBox;
            if( _previousKeyboardState.IsKeyDown( Keys.D3 ) && ks.IsKeyUp( Keys.D3 ) )
                _renderParamsTemplate.Mode ^= RenderMode.TraceRenderingExtent;

            _previousKeyboardState = ks;
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            Matrix worldToCamera = Matrix.Identity;

            _spriteBatch.Begin();

            if( _contentLoaded )
            {
                _spriteBatch.DrawPolygonExtent( _polygon, worldToCamera, _renderParamsTemplate );
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
