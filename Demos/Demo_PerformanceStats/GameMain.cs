using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace Demo_PerformanceStats
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

    public enum Fonts : int
    {
        [ContentIdentifier( "Arial" )]
        Arial,
        [ContentIdentifier( "TimesNewRoman" )]
        TimesNewRoman,
        [ContentIdentifier( "CourierNew" )]
        CourierNew
    }

    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        SpriteFontCache _fonts;
        PerformanceTimer _gameUpdatePerformance, _gameDrawPerformance;
        XenString _FPS, _update, _draw, _suffix, _space;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            IsMouseVisible = true;

            _gameUpdatePerformance = new PerformanceTimer();
            _gameDrawPerformance = new PerformanceTimer();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );

            _fonts = new SpriteFontCache( typeof( Fonts ) );
            _FPS = XenString.Acquire();
            _FPS.Reset( _fonts[ (int)Fonts.Arial ], "FPS: " );
            _update = XenString.Acquire();
            _update.Reset( _fonts[ (int)Fonts.Arial ], "Update: " );
            _draw = XenString.Acquire();
            _draw.Reset( _fonts[ (int)Fonts.Arial ], "Draw: " );
            _suffix = XenString.Acquire();
            _suffix.Reset( _fonts[ (int)Fonts.Arial ], "ms " );
            _space = XenString.Acquire();
            _space.Reset( _fonts[ (int)Fonts.Arial ], " " );
        }

        protected override void UnloadContent()
        {
            _FPS.Release();
            _update.Release();
            _draw.Release();
            _suffix.Release();
            _space.Release();
            base.UnloadContent();
        }

        protected override void Update( GameTime gameTime )
        {
            _gameUpdatePerformance.Begin();
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                this.Exit();

            base.Update( gameTime );
            _gameUpdatePerformance.End();
        }

        protected override void Draw( GameTime gameTime )
        {
            _gameDrawPerformance.Begin();
            GraphicsDevice.Clear( Color.CornflowerBlue );

            base.Draw( gameTime );
            _gameDrawPerformance.End();

            // Refresh here as the end-of-frame
            PerformanceTimer.FrameRefresh();
            _gameUpdatePerformance.Refresh();
            _gameDrawPerformance.Refresh();

            // Draw the results onto the screen
            _spriteBatch.Begin();

            _spriteBatch.DrawString( _FPS.SpriteFont,
                _FPS & XenString.Temporary( PerformanceTimer.CurrentFramerate, 2 ) & _space &
                XenString.Temporary( PerformanceTimer.MinFramerate, 2 ) & _space &
                XenString.Temporary( PerformanceTimer.MaxFramerate, 2 ) & _space &
                XenString.Temporary( PerformanceTimer.AverageFramerate, 2 ), Vector2.Zero, Color.White );
            _spriteBatch.DrawString( _update.SpriteFont,
                _update & XenString.Temporary( _gameUpdatePerformance.Current, 2 ) & _space &
                XenString.Temporary( _gameUpdatePerformance.Min, 2 ) & _space &
                XenString.Temporary( _gameUpdatePerformance.Max, 2 ) & _space &
                XenString.Temporary( _gameUpdatePerformance.Average, 2 ), Vector2.UnitY * 20, Color.White );
            _spriteBatch.DrawString( _draw.SpriteFont,
                _draw & XenString.Temporary( _gameDrawPerformance.Current, 2 ) & _suffix & _space &
                XenString.Temporary( _gameDrawPerformance.Min, 2 ) & _suffix & _space &
                XenString.Temporary( _gameDrawPerformance.Max, 2 ) & _suffix & _space &
                XenString.Temporary( _gameDrawPerformance.Average, 2 ) & _suffix, Vector2.UnitY * 40, Color.White );

            //_spriteBatch.DrawString( _font, "CurFPS: " + PerformanceTimer.CurrentFramerate.ToString( "0.00" ), Vector2.Zero, Color.White );
            //_spriteBatch.DrawString( _font, "MinFPS: " + PerformanceTimer.MinFramerate.ToString( "0.00" ), Vector2.UnitY * 20, Color.White );
            //_spriteBatch.DrawString( _font, "MaxFPS: " + PerformanceTimer.MaxFramerate.ToString( "0.00" ), Vector2.UnitY * 40, Color.White );
            //_spriteBatch.DrawString( _font, "AvgFPS: " + PerformanceTimer.AverageFramerate.ToString( "0.00" ), Vector2.UnitY * 60, Color.White );

            //_spriteBatch.DrawString( _font, "CurUpdate: " + _gameUpdatePerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 100, Color.Blue );
            //_spriteBatch.DrawString( _font, "MinUpdate: " + _gameUpdatePerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 120, Color.Blue );
            //_spriteBatch.DrawString( _font, "MaxUpdate: " + _gameUpdatePerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 140, Color.Blue );
            //_spriteBatch.DrawString( _font, "AvgUpdate: " + _gameUpdatePerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 160, Color.Blue );

            //_spriteBatch.DrawString( _font, "CurDraw: " + _gameDrawPerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 200, Color.Red );
            //_spriteBatch.DrawString( _font, "MinDraw: " + _gameDrawPerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 220, Color.Red );
            //_spriteBatch.DrawString( _font, "MaxDraw: " + _gameDrawPerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 240, Color.Red );
            //_spriteBatch.DrawString( _font, "AvgDraw: " + _gameDrawPerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 260, Color.Red );
            _spriteBatch.End();

            // Let the next 'frame' begin now because Draw() is throttled internally
            PerformanceTimer.FrameTick();
        }
    }
}
