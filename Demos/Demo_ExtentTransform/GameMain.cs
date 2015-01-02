using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_ExtentTransform
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

    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;

        StaticSprite _sprite;
        StaticSprite _markerAnchor_Blue;
        StaticSprite _markerTopLeft_Red;
        Texture2DCache _textures;

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
            _sprite = StaticSprite.Acquire( _textures[ (int)Textures.ColoredRect ], new Vector2( 100, 100 ) );
            _sprite.LayerDepth = 1;

            _markerAnchor_Blue = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Blue ], new Vector2( 4, 4 ) );
            _markerAnchor_Blue.LayerDepth = 0;

            _markerTopLeft_Red = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Red ], new Vector2( 4, 4 ) );
            _markerTopLeft_Red.LayerDepth = 0;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            #region MoveSprite
            if( ks.IsKeyDown( Keys.A ) )
            {
                _sprite.RenderingExtent.Anchor -= new Vector2( 5, 0 );
            }
            if( ks.IsKeyDown( Keys.D ) )
            {
                _sprite.RenderingExtent.Anchor += new Vector2( 5, 0 );
            }
            if( ks.IsKeyDown( Keys.W ) )
            {
                _sprite.RenderingExtent.Anchor += new Vector2( 0, -5 );
            }
            if( ks.IsKeyDown( Keys.S ) )
            {
                _sprite.RenderingExtent.Anchor += new Vector2( 0, 5 );
            }
            if( ks.IsKeyDown( Keys.Q ) )
            {
                _sprite.RenderingExtent.Angle -= 0.04f;
            }
            if( ks.IsKeyDown( Keys.E ) )
            {
                _sprite.RenderingExtent.Angle += 0.04f;
            }

            if( ks.IsKeyDown( Keys.Z ) )
            {
                _sprite.RenderingExtent.Scale *= 1.04f;
            }
            if( ks.IsKeyDown( Keys.C ) )
            {
                _sprite.RenderingExtent.Scale /= 1.04f;
            }

            if( ks.IsKeyDown( Keys.F ) )
            {
                _sprite.RenderingExtent.Scale = new Vector2( _sprite.RenderingExtent.Scale.X, _sprite.RenderingExtent.Scale.Y * 1.04f );
            }
            if( ks.IsKeyDown( Keys.G ) )
            {
                _sprite.RenderingExtent.Scale = new Vector2( _sprite.RenderingExtent.Scale.X, _sprite.RenderingExtent.Scale.Y / 1.04f );
            }
            if( ks.IsKeyDown( Keys.V ) )
            {
                _sprite.RenderingExtent.Scale = new Vector2( _sprite.RenderingExtent.Scale.X * 1.04f, _sprite.RenderingExtent.Scale.Y );
            }
            if( ks.IsKeyDown( Keys.B ) )
            {
                _sprite.RenderingExtent.Scale = new Vector2( _sprite.RenderingExtent.Scale.X / 1.04f, _sprite.RenderingExtent.Scale.Y );
            }
            #endregion
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            if( null != _sprite )
            {
                _spriteBatch.DrawSprite( _sprite, RenderParams.Debug );
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
