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
using Xen2D;
using System.Globalization;
using System.IO;

namespace TextRender
{
    public enum Fonts : int
    {
        [ContentIdentifier( "Arial" )]
        Arial,
        [ContentIdentifier( "TimesNewRoman" )]
        TimesNewRoman,
        [ContentIdentifier( "CourierNew" )]
        CourierNew
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteBatch _spriteBatch;
        FontCache _fonts;
        XenString _sampleString;
        XenString _space;

        public Game1()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch( GraphicsDevice );

            _fonts = new FontCache( typeof( Fonts ) );
            _sampleString = XenString.Acquire();
            _sampleString.Reset( _fonts[ (int)Fonts.Arial ], Strings.Strings.Sample );
            _space = XenString.Acquire();
            _space.Reset( _fonts[ (int)Fonts.Arial ], " " );
        }

        protected override void Update( GameTime gameTime )
        {
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                this.Exit();

            KeyboardState keyboardState = Keyboard.GetState();
            if( keyboardState.IsKeyDown( Keys.D1 ) )
            {
                Strings.Strings.Culture = CultureInfo.CurrentCulture;
                _sampleString.Reset( _sampleString.SpriteFont, Strings.Strings.Sample );
            }
            if( keyboardState.IsKeyDown( Keys.D2 ) )
            {
                Strings.Strings.Culture = CultureInfo.CreateSpecificCulture( "fr" );
                _sampleString.Reset( _sampleString.SpriteFont, Strings.Strings.Sample );
            }
            //if( keyboardState.IsKeyDown( Keys.D3 ) )
            //{
            //    Strings.Strings.Culture = CultureInfo.CreateSpecificCulture( "ko" );
            //}
            //if( keyboardState.IsKeyDown( Keys.D4 ) )
            //{
            //    Strings.Strings.Culture = CultureInfo.CreateSpecificCulture( "ja" );
            //}
            if( keyboardState.IsKeyDown( Keys.D3 ) )
            {
                _sampleString.SpriteFont = _fonts[ (int)Fonts.Arial ];
                _space.SpriteFont = _fonts[ (int)Fonts.Arial ];
            }
            if( keyboardState.IsKeyDown( Keys.D4 ) )
            {
                _sampleString.SpriteFont = _fonts[ (int)Fonts.CourierNew ];
                _space.SpriteFont = _fonts[ (int)Fonts.CourierNew ];
            }
            if( keyboardState.IsKeyDown( Keys.D5 ) )
            {
                _sampleString.SpriteFont = _fonts[ (int)Fonts.TimesNewRoman ];
                _space.SpriteFont = _fonts[ (int)Fonts.TimesNewRoman ];
            }

            base.Update( gameTime );
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            //_spriteBatch.DrawString( _sampleString );

            //XenString tempString = XenString.Acquire();
            //XenString tempString = XenString.Temporary( _fonts[ (int)Fonts.CourierNew ], 12345 );
            //tempString.Release();
            //int capacity = XenString.Pool.Capacity;

            //_spriteBatch.DrawString( XenString.Temporary( _fonts[ (int)Fonts.CourierNew ], 12345 ) );
            //_spriteBatch.DrawString( _sampleString & XenString.Temporary( _fonts[ (int)Fonts.CourierNew ], MathHelper.Pi ) );
            _spriteBatch.DrawString( _sampleString.SpriteFont, XenString.Temporary( MathHelper.Pi ) & _space & XenString.Temporary( 12345 ) & _space & XenString.Temporary( 0.12345f ) );
            //_spriteBatch.DrawString( _fonts[ (int)Fonts.Arial ], Strings.Strings.Sample, Vector2.Zero, Color.White );

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
