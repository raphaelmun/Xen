using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace Demo_Tweens
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

    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        PerformanceTimer _gameDrawPerformance, _tweenPerformance;
        const float StepInterval = 0.01f;
        Vector2 _pStart, _pEnd;
        SpriteFont _font;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            IsMouseVisible = true;

            _gameDrawPerformance = new PerformanceTimer();
            _tweenPerformance = new PerformanceTimer();
            _tweenPerformance.CountEachExecution = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );

            _font = Globals.Content.Load<SpriteFont>( "Arial" );
            _pStart = Vector2.UnitY * (float)GraphicsDevice.Viewport.Height;
            _pEnd = Vector2.UnitX * (float)GraphicsDevice.Viewport.Width;
        }

        protected override void Update( GameTime gameTime )
        {
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                this.Exit();

            base.Update( gameTime );
        }

        protected override void Draw( GameTime gameTime )
        {
            _gameDrawPerformance.Begin();
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            for( float i = 0.0f; i < 1.0f; i += StepInterval )
            {
                _tweenPerformance.Begin();
                float x1 = i, x2 = i + StepInterval;
                Vector2 start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Step( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                Vector2 end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Step( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Blue, start, end, 3 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Linear( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Linear( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Yellow, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Sine( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Sine( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.SineIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.SineIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.SineOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.SineOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.SineInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.SineInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );
                
                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Quadratic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Quadratic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.QuadraticInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Cubic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Cubic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CubicIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CubicIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CubicOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CubicOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CubicInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CubicInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Exponential( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Exponential( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.ExponentialInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Logarithmic( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Logarithmic( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.LogarithmicInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.Circular( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.Circular( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CircularIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CircularIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CircularOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CircularOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Interpolator.CircularInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Interpolator.CircularInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );
                _tweenPerformance.End();
            }

            _spriteBatch.End();

            base.Draw( gameTime );
            _gameDrawPerformance.End();

            // Refresh here as the end-of-frame
            PerformanceTimer.FrameRefresh();
            _gameDrawPerformance.Refresh();
            _tweenPerformance.Refresh();

            // Draw the results onto the screen
            _spriteBatch.Begin();
            _spriteBatch.DrawString( _font, "CurFPS: " + PerformanceTimer.CurrentFramerate.ToString( "0.00" ), Vector2.Zero, Color.White );
            _spriteBatch.DrawString( _font, "MinFPS: " + PerformanceTimer.MinFramerate.ToString( "0.00" ), Vector2.UnitY * 20, Color.White );
            _spriteBatch.DrawString( _font, "MaxFPS: " + PerformanceTimer.MaxFramerate.ToString( "0.00" ), Vector2.UnitY * 40, Color.White );
            _spriteBatch.DrawString( _font, "AvgFPS: " + PerformanceTimer.AverageFramerate.ToString( "0.00" ), Vector2.UnitY * 60, Color.White );

            _spriteBatch.DrawString( _font, "CurDraw: " + _gameDrawPerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 100, Color.Black );
            _spriteBatch.DrawString( _font, "MinDraw: " + _gameDrawPerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 120, Color.Black );
            _spriteBatch.DrawString( _font, "MaxDraw: " + _gameDrawPerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 140, Color.Black );
            _spriteBatch.DrawString( _font, "AvgDraw: " + _gameDrawPerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 160, Color.Black );

            _spriteBatch.DrawString( _font, "CurTween: " + _tweenPerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 200, Color.Black );
            _spriteBatch.DrawString( _font, "MinTween: " + _tweenPerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 220, Color.Black );
            _spriteBatch.DrawString( _font, "MaxTween: " + _tweenPerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 240, Color.Black );
            _spriteBatch.DrawString( _font, "AvgTween: " + _tweenPerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 260, Color.Black );
            _spriteBatch.DrawDecimal( _font, _tweenPerformance.Average, 2, Vector2.UnitY * 280, Color.Black );
            _spriteBatch.DrawDecimal( _font, PerformanceTimer.CurrentFramerate, 2, Vector2.UnitY * 300, Color.Black );
            _spriteBatch.End();

            // Let the next 'frame' begin now because Draw() is throttled internally
            PerformanceTimer.FrameTick();
        }
    }
}
