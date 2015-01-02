using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using Xen3D;

namespace Demo_3DModels
{
    public enum Models : int
    {
        [ContentIdentifier( "models\\Crate" )]
        Crate,
        [ContentIdentifier( "models\\claypot" )]
        ClayPot,
    }

    /// <summary>
    /// This Demo shows how to place and draw a static model
    /// </summary>
    public class GameMain : Game
    {
        ModelCache _models;
        SpriteBatch _spriteBatch;
        StaticModel _crate = null;
        StaticModel _clayPot = null;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );
            _models = new ModelCache( typeof( Models ) );

            _crate = StaticModel.Acquire( _models[ (int)Models.Crate ] );
            _crate.LayerDepth = 0; //front
            _clayPot = StaticModel.Acquire( _models[ (int)Models.ClayPot ] );
            _clayPot.LayerDepth = 1; //back
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            //ISSUE: DisplayAttributes.LayerDepth does nothing
            //_spriteBatch.DrawSprite( _spriteRed );
            //_spriteBatch.DrawSprite( _spriteBlue );

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}