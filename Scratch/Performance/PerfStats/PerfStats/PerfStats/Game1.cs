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
using System.Text;

namespace PerfStats
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteBatch _spriteBatch;
        PerformanceTimer _gameUpdatePerformance, _gameDrawPerformance;
        SpriteFont _font;

        public Game1()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _gameUpdatePerformance = new PerformanceTimer();
            _gameDrawPerformance = new PerformanceTimer();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch( GraphicsDevice );

            // TODO: use this.Content to load your game content here
            _font = Globals.Content.Load<SpriteFont>( "Arial" );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            _gameUpdatePerformance.Begin();
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                this.Exit();

            // TODO: Add your update logic here

            base.Update( gameTime );
            _gameUpdatePerformance.End();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            _gameDrawPerformance.Begin();
            GraphicsDevice.Clear( Color.CornflowerBlue );

            // TODO: Add your drawing code here

            base.Draw( gameTime );
            _gameDrawPerformance.End();

            // Refresh here as the end-of-frame
            PerformanceTimer.FrameRefresh();
            _gameUpdatePerformance.Refresh();
            _gameDrawPerformance.Refresh();

            // Draw the results onto the screen
            _spriteBatch.Begin();
            _spriteBatch.DrawString( _font, "CurFPS: " + PerformanceTimer.CurrentFramerate.ToString( "0.00" ), Vector2.Zero, Color.White );
            _spriteBatch.DrawString( _font, "MinFPS: " + PerformanceTimer.MinFramerate.ToString( "0.00" ), Vector2.UnitY * 20, Color.White );
            _spriteBatch.DrawString( _font, "MaxFPS: " + PerformanceTimer.MaxFramerate.ToString( "0.00" ), Vector2.UnitY * 40, Color.White );
            _spriteBatch.DrawString( _font, "AvgFPS: " + PerformanceTimer.AverageFramerate.ToString( "0.00" ), Vector2.UnitY * 60, Color.White );

            _spriteBatch.DrawString( _font, "CurUpdate: " + _gameUpdatePerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 100, Color.Blue );
            _spriteBatch.DrawString( _font, "MinUpdate: " + _gameUpdatePerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 120, Color.Blue );
            _spriteBatch.DrawString( _font, "MaxUpdate: " + _gameUpdatePerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 140, Color.Blue );
            _spriteBatch.DrawString( _font, "AvgUpdate: " + _gameUpdatePerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 160, Color.Blue );

            _spriteBatch.DrawString( _font, "CurDraw: " + _gameDrawPerformance.Current.ToString( "0.00ms" ), Vector2.UnitY * 200, Color.Red );
            _spriteBatch.DrawString( _font, "MinDraw: " + _gameDrawPerformance.Min.ToString( "0.00ms" ), Vector2.UnitY * 220, Color.Red );
            _spriteBatch.DrawString( _font, "MaxDraw: " + _gameDrawPerformance.Max.ToString( "0.00ms" ), Vector2.UnitY * 240, Color.Red );
            _spriteBatch.DrawString( _font, "AvgDraw: " + _gameDrawPerformance.Average.ToString( "0.00ms" ), Vector2.UnitY * 260, Color.Red );
            _spriteBatch.End();

            // Let the next 'frame' begin now because Draw() is throttled internally
            PerformanceTimer.FrameTick();
        }
    }
}
