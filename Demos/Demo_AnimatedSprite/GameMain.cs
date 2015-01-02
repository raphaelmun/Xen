using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace Demo_AnimatedSprite
{
    #region entry point
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main( string[] args )
        {
            using( GameMain game = new GameMain() )
            {
                game.Run();
            }
        }
    }
#endif
    #endregion

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
        Texture2D _animationStripTexture;
        AnimatedSprite _sprite = new AnimatedSprite();
        MouseState _lastMouseState;
        Texture2DCache _textureCache = null;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );

            _textureCache = new Texture2DCache( typeof( Textures ) );

            _animationStripTexture = _textureCache[ (int)Textures.Explosion ];
            _sprite.ResetFromCenter( _animationStripTexture, 5, 5, new Vector2( 100, 100 ) );
        }

        MouseState ms;
        KeyboardState ks;

        protected override void Update( GameTime gameTime )
        {   
            base.Update( gameTime );

            _sprite.Update( gameTime );

            ms = Mouse.GetState();
            ks = Keyboard.GetState();

            if( ( _lastMouseState.LeftButton == ButtonState.Pressed ) &&
                ( ms.LeftButton == ButtonState.Released ) )
            {
                _sprite.ResetFromCenter( _animationStripTexture, 5, 5, new Vector2( ms.X, ms.Y ) );
            }

            _lastMouseState = ms;
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.Black );

            _spriteBatch.Begin();

            _sprite.Draw( _spriteBatch );

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
