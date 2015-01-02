using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_MouseBasics
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\cursor" )]
        Cursor,
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

    #region Font Cache

    enum FontId : int
    {
        [ContentIdentifier( "Arial" )]
        Arial
    }

    class Fonts : SpriteFontCache<FontId>
    {
        public static readonly Fonts Instance = new Fonts();

        public static SpriteFont Get( FontId font )
        {
            return Instance[ font ];
        }

        public override SpriteFont this[ FontId font ]
        {
            get { return this[ (int)font ]; }
        }
    }
    #endregion

    /// <summary>
    /// This demo replaces the default windows mouse with a XenMouse using a 
    /// custom cursor texture and displays its current position.  
    /// </summary>
    public class GameMain : GameBase
    {
        XenMouse _mouse;
        XenString _openParen;
        XenString _closeParen;
        XenString _comma;
        Action<Vector2> _mouseMovedHandler;

        public GameMain()
        {
            IsMouseVisible = false;
            _mouseMovedHandler = new Action<Vector2>( OnMouseMoved );
        }

        private void OnMouseMoved( Vector2 position ) { }

        protected override void LoadContent()
        {
            _mouse = XenMouse.Acquire();
            _mouse.Reset( StaticSprite.Acquire( Textures.Get( TexId.Cursor ) ) );
            _mouse.OnMouseMove.Add( _mouseMovedHandler );

            _openParen = XenString.Acquire();
            _openParen.Reset( Fonts.Get( FontId.Arial ), "(" );

            _closeParen = XenString.Acquire();
            _closeParen.Reset( Fonts.Get( FontId.Arial ), ")" );

            _comma = XenString.Acquire();
            _comma.Reset( Fonts.Get( FontId.Arial ), "," );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            _mouse.Update( gameTime );
        }

        protected override void DrawInternal( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.WhiteSmoke );
            _mouse.Draw( SpriteRenderer );

            Vector2 textOffset = new Vector2( 20, -20 );

            SpriteRenderer.DrawString( _openParen.SpriteFont,
                _openParen & XenString.Temporary( _mouse.Position.X, 0 ) & _comma &
                XenString.Temporary( _mouse.Position.Y, 0 ) & _closeParen, _mouse.Position + textOffset, Color.Black );
        }
    }
}
