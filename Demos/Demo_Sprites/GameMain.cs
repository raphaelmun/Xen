using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Demo_Sprites
{
    public enum Textures : int
    {
        [ContentIdentifier( "textures\\rect_blue_horizontal_200x100" )]
        RectBlueHorizontal,
        [ContentIdentifier( "textures\\rect_red_vertical_100x200" )]
        RectRedVertical,
    }

    /// <summary>
    /// This Demo shows how to place and draw a static sprite.  
    /// </summary>
    public class GameMain : Game
    {
        Texture2DCache _textures;
        SpriteBatch _spriteBatch;
        StaticSprite _spriteRed = null;
        StaticSprite _spriteBlue = null;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );
            _textures = new Texture2DCache( typeof( Textures ) );
            _spriteRed = StaticSprite.Acquire( _textures[ (int)Textures.RectRedVertical ] );

            _spriteRed.LayerDepth = 0; //front
            _spriteBlue = StaticSprite.Acquire( _textures[ (int)Textures.RectBlueHorizontal ] );
            _spriteBlue.LayerDepth = 1; //back
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            //ISSUE: DisplayAttributes.LayerDepth does nothing
            _spriteBatch.DrawSprite( _spriteRed );
            _spriteBatch.DrawSprite( _spriteBlue );

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
