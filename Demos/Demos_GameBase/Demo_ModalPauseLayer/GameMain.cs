using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace Demo_ModalPauseLayer
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\cursor" )]
        Cursor,
        [ContentIdentifier( "textures\\gray_back_800x480" )]
        GrayBackground,
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
    /// This demo shows how to use a transparent element to create an overlay effect when the game is paused.
    /// Press "Space" to pause the game.  
    /// </summary>
    public class GameMain : GameBase
    {
        Layer _pauseLayer;

        protected override void LoadContent()
        {
            var worldLayer = Layer.Acquire();
            worldLayer.Children.Add( MovingElement.Acquire() );
            worldLayer.DrawOrder = 0;

            _pauseLayer = Layer.Acquire();
            _pauseLayer.Children.Add( BackgroundElement.Acquire() );
            _pauseLayer.DrawOrder = 5;
            _pauseLayer.Visible = false;

            MainScene.Children.Add( worldLayer );
            MainScene.Children.Add( _pauseLayer );
        }

        protected override void ProcessInput( ref InputState input )
        {
            base.ProcessInput( ref input );
            if( input.IsKeyPressed( Keys.Space ) )
            {
                IsPaused = !IsPaused;
                _pauseLayer.Visible = !_pauseLayer.Visible;
            }
        }
    }
}
