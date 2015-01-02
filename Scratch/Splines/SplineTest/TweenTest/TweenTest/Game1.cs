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

namespace TweenTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteBatch _spriteBatch;
        const float stepInterval = 0.01f;
        Vector2 _pStart, _pEnd;

        public Game1()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            //Globals.Graphics.PreferredBackBufferWidth = Globals.Graphics.PreferredBackBufferHeight = 640;
            //Globals.Graphics.ApplyChanges();
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
            _pStart = Vector2.UnitY * (float)GraphicsDevice.Viewport.Height;
            _pEnd = Vector2.UnitX * (float)GraphicsDevice.Viewport.Width;

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
            // Allows the game to exit
            if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                this.Exit();

            // TODO: Add your update logic here

            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for( float i = 0.0f; i < 1.0f; i += stepInterval )
            {
                float x1 = i, x2 = i + stepInterval;
                Vector2 start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Step( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                Vector2 end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Step( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Blue, start, end, 3 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Linear( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Linear( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Yellow, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Sine( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Sine( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.SineIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.SineIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.SineOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.SineOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.SineInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.SineInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Quadratic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Quadratic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.QuadraticIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.QuadraticIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.QuadraticOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.QuadraticOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.QuadraticInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.QuadraticInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Cubic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Cubic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CubicIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CubicIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CubicOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CubicOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CubicInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CubicInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Exponential( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Exponential( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.ExponentialIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.ExponentialIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.ExponentialOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.ExponentialOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.ExponentialInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.ExponentialInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Logarithmic( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Logarithmic( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.LogarithmicInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.Circular( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.Circular( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                _spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CircularIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CircularIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CircularOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CircularOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * GraphicsDevice.Viewport.Width, Tweener.CircularInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * GraphicsDevice.Viewport.Width, Tweener.CircularInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                _spriteBatch.DrawLine( Color.Purple, start, end, 2 );
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
