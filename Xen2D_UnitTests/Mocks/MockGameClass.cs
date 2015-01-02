using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Xen2D_UnitTests
{
    internal class TestConstants
    {
        internal const int ResolutionWidth = 1280;
        internal const int ResolutionHeight = 720;
        internal static readonly Vector2 CenterOfScreen = new Vector2( ResolutionWidth / 2.0f, ResolutionHeight / 2.0f );
        internal const long UpdateTicks = 250;
    }

    /// <summary>
    /// Dummy Game for Testing that quits after a couple updates
    /// </summary>
    internal class DummyGame : Microsoft.Xna.Framework.Game
    {
        internal GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;

        internal bool WaitForInput;
        internal bool HasInitialized;
        internal bool HasLoadedContent;
        internal bool HasUnloadedContent;
        internal bool HasUpdated;
        internal bool HasDrawn;

        internal DummyGame()
            : this( false )
        { }

        internal DummyGame( bool waitForInput )
            : base()
        {
            graphics = new GraphicsDeviceManager( this );
            // Contents of the "Content" directory is at the main directory now
            Content.RootDirectory = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );

            graphics.PreferredBackBufferWidth = TestConstants.ResolutionWidth;
            graphics.PreferredBackBufferHeight = TestConstants.ResolutionHeight;
            graphics.ApplyChanges();

            WaitForInput = waitForInput;
            HasInitialized = false;
            HasLoadedContent = false;
            HasUnloadedContent = false;
            HasUpdated = false;
            HasDrawn = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            HasInitialized = true;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );

            base.LoadContent();
            HasLoadedContent = true;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            HasUnloadedContent = true;
        }

        protected override void Update( GameTime gameTime )
        {
            if( WaitForInput )
            {
                // See if any input was pressed
                if( Keyboard.GetState().GetPressedKeys().Length > 0 )
                {
                    this.Exit();
                }
                for( int i = 0; i <= (int)PlayerIndex.Four; i++ )
                {
                    GamePadState gamePadState = GamePad.GetState( (PlayerIndex)i );
                    if( gamePadState.IsConnected )
                    {
                        if( gamePadState.Buttons.A == ButtonState.Pressed ||
                            gamePadState.Buttons.B == ButtonState.Pressed ||
                            gamePadState.Buttons.Back == ButtonState.Pressed ||
                            gamePadState.Buttons.BigButton == ButtonState.Pressed ||
                            gamePadState.Buttons.LeftShoulder == ButtonState.Pressed ||
                            gamePadState.Buttons.LeftStick == ButtonState.Pressed ||
                            gamePadState.Buttons.RightShoulder == ButtonState.Pressed ||
                            gamePadState.Buttons.RightStick == ButtonState.Pressed ||
                            gamePadState.Buttons.Start == ButtonState.Pressed ||
                            gamePadState.Buttons.X == ButtonState.Pressed ||
                            gamePadState.Buttons.Y == ButtonState.Pressed )
                        {
                            this.Exit();
                        }
                    }
                }
            }
            else
            {
                // Auto-Quit
                TimeSpan timeSpan = new TimeSpan(TestConstants.UpdateTicks);
                gameTime = new GameTime( timeSpan, timeSpan );
                this.Exit();
            }

            base.Update( gameTime );
            HasUpdated = true;
        }

        protected override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );
            HasDrawn = true;
        }
    }

    public delegate void InjectedDummyGameInitialize();
    public delegate void InjectedDummyGameLoadContent( ContentManager content );
    public delegate void InjectedDummyGameUnloadContent();
    public delegate void InjectedDummyGameUpdate( GameTime gameTime );
    public delegate void InjectedDummyGameDraw( GameTime gameTime, SpriteBatch spriteBatch, bool isFirstFrame );

    /// <summary>
    /// Dummy Game with for Testing that quits after a couple updates
    /// </summary>
    internal class InjectableDummyGame : DummyGame
    {
        static InjectableDummyGame _instance = new InjectableDummyGame();
        public static InjectableDummyGame Instance
        {
            get { return _instance; }
        }

        internal InjectedDummyGameInitialize InitializeMethod;
        internal InjectedDummyGameLoadContent LoadContentMethod;
        internal InjectedDummyGameUnloadContent UnloadContentMethod;
        internal InjectedDummyGameUpdate UpdateMethod;
        internal InjectedDummyGameDraw DrawMethod;
        bool isFirstFrame;

        internal InjectableDummyGame()
            : this( false, null, null, null, null, null )
        { }

        internal InjectableDummyGame( bool waitForInput )
            : this( waitForInput, null, null, null, null, null )
        { }

        internal InjectableDummyGame(
            bool waitForInput,
            InjectedDummyGameInitialize initializeMethod,
            InjectedDummyGameLoadContent loadContentMethod,
            InjectedDummyGameUnloadContent unloadContentMethod,
            InjectedDummyGameUpdate updateMethod,
            InjectedDummyGameDraw drawMethod )
            : base( waitForInput )
        {
            InitializeMethod = initializeMethod;
            LoadContentMethod = loadContentMethod;
            UnloadContentMethod = unloadContentMethod;
            UpdateMethod = updateMethod;
            DrawMethod = drawMethod;
            isFirstFrame = true;
        }

        protected override void Initialize()
        {
            if( InitializeMethod != null )
                InitializeMethod();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if( LoadContentMethod != null )
                LoadContentMethod( Content );
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if( UnloadContentMethod != null )
                UnloadContentMethod();
            base.UnloadContent();
        }

        protected override void Update( GameTime gameTime )
        {
            if( UpdateMethod != null )
                UpdateMethod( gameTime );
            base.Update( gameTime );
        }

        protected override void Draw( GameTime gameTime )
        {
            if( DrawMethod != null )
                DrawMethod( gameTime, spriteBatch, isFirstFrame );

            base.Draw( gameTime );
            isFirstFrame = false;
        }
    }
}
