using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    enum XenFonts : int
    {
        [ContentIdentifier( "Arial" )]
        Arial
    }

    /// <summary>
    /// Base class for all Xen games.
    /// </summary>
    public abstract class GameBase : Game
    {
        public const string DefaultContentPath = "Content";
        public const int DefaultResolutionWidth = 800;
        public const int DefaultResolutionHeight = 480;

        protected static bool InitGraphicsMode( int width, int height )
        {
            return InitGraphicsMode( Globals.Graphics, width, height, false );
        }

        /// <summary>
        /// Attempt to set the display mode to the desired resolution.  Iterates through the display
        /// capabilities of the default graphics adapter to determine if the graphics adapter supports the
        /// requested resolution.  If so, the resolution is set and the function returns true.  If not,
        /// no change is made and the function returns false.
        /// </summary>
        /// <param name="gdm">The Graphics device to use.</param>
        /// <param name="iWidth">Desired screen width.</param>
        /// <param name="iHeight">Desired screen height.</param>
        /// <param name="bFullScreen">True if you wish to go to Full Screen, false for Windowed Mode.</param>
        protected static bool InitGraphicsMode( GraphicsDeviceManager gdm, int iWidth, int iHeight, bool bFullScreen )
        {
            if( null == gdm )
                throw new ArgumentNullException( "gdm" );
#if !SILVERLIGHT
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if( bFullScreen == false )
            {
                if( ( iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width )
                    && ( iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height ) )
                {
#endif
                    gdm.PreferredBackBufferWidth = iWidth;
                    gdm.PreferredBackBufferHeight = iHeight;
                    gdm.IsFullScreen = bFullScreen;
                    gdm.ApplyChanges();
#if !SILVERLIGHT
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach( DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes )
                {
                    // Check the width and height of each mode against the passed values
                    if( ( dm.Width == iWidth ) && ( dm.Height == iHeight ) )
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        gdm.PreferredBackBufferWidth = iWidth;
                        gdm.PreferredBackBufferHeight = iHeight;
                        gdm.IsFullScreen = bFullScreen;
                        gdm.ApplyChanges();
                        return true;
                    }
                }
            }
#endif
            return false;
        }

        private GraphicsDeviceManager           _graphics;
        private InputState                      _inputState;
        private SpriteBatch                     _spriteBatch;
        private bool                            _isFirstUpdate = true;
        private Layer                           _mainScene;
        private XenMouse                        _mouse;
        private bool                            _isPaused = false;
        private bool                            _canPause = true;
        private Action<CollisionEventArgs>      _onMainSceneCollision;
        private Dictionary<string, IXenPlugin>  _plugins;
        private WeakReference                   _gcTracker;
#if SILVERLIGHT
        private ContentManager                  _embeddedXenContent = null;
#else
        private ResourceContentManager          _embeddedXenContent = null;
#endif
        private SpriteFontCache                 _xenFonts = null;
        private PerformanceTimer                _updatePerformanceTimer, _drawPerformanceTimer;

        protected PerformanceTimer UpdatePerformance { get { return _updatePerformanceTimer; } }
        protected PerformanceTimer DrawPerformance { get { return _drawPerformanceTimer; } }
        protected Color BackgroundColor { get; set; }
        protected Layer MainScene { get { return _mainScene; } }
        public string PluginDirectory { get; set; }
        protected SpriteBatch SpriteRenderer { get { return _spriteBatch; } }
        protected bool UseMouse { get; set; }
        protected XenMouse XenMouse { get { return _mouse; } }
        
        public bool CanPause 
        { 
            get { return _canPause; } 
            protected set 
            { 
                _canPause = value;
                if( !_canPause ) IsPaused = false;
            } 
        }

        public bool IsPaused { 
            get { return _isPaused; } 
            set
            {
                _isPaused = ( value && CanPause );
            }
        }

        public Color DebugColor
        {
            get
            {
                return _mainScene.DebugColor;
            }
            set
            {
                _mainScene.DebugColor = value;
            }
        }
        public bool ShowDebug
        {
            get
            {
                return _mainScene.ShowDebug;
            }
            set
            {
                _mainScene.ShowDebug = value;
            }
        }

        public GameBase()
        {
            _graphics = new GraphicsDeviceManager( this );
            IsMouseVisible = true;
            UseMouse = true;
            Content.RootDirectory = DefaultContentPath;
            BackgroundColor = Color.CornflowerBlue;
            Globals.Content = Content;
            Globals.Graphics = _graphics;
            _onMainSceneCollision = new Action<CollisionEventArgs>( OnMainSceneCollisionHandler );
            _plugins = new Dictionary<string, IXenPlugin>();
#if !SILVERLIGHT && !WINDOWS_PHONE
            PluginDirectory = Path.Combine( Directory.GetCurrentDirectory(), "Plugins" ); // local plugins directory by default
#endif
            _updatePerformanceTimer = new PerformanceTimer();
            _drawPerformanceTimer = new PerformanceTimer();
        }

        protected virtual void OnGarbageCollectionRun() { }

        public void CallPluginFunction( string pluginName, int functionalityID, params object[] parameterList )
        {
            if( _plugins.ContainsKey( pluginName ) )
            {
                _plugins[ pluginName ].Call( functionalityID, parameterList );
            }
            else
            {
                throw new ArgumentOutOfRangeException( "Plugin Not Loaded" );
            }
        }

        protected virtual void OnMainSceneCollisionHandler( CollisionEventArgs collisionEvent ){ }

        protected override void Initialize()
        {
            Globals.GraphicsDevice = GraphicsDevice;
            _spriteBatch = new SpriteBatch( GraphicsDevice );

            InitGraphicsMode( DefaultResolutionWidth, DefaultResolutionHeight );

            _mouse = XenMouse.Acquire();
            _mainScene = Layer.Acquire();
            _mainScene.OnCollision.Add( _onMainSceneCollision );
#if !SILVERLIGHT
            //ISSUE: how will this work on the extended devices (phone/xbox 360) where we have no access to the file system?
            if( Directory.Exists( PluginDirectory ) )
            {
                LoadPlugins();
            }
#endif

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            // Load embedded content
#if SILVERLIGHT
            _embeddedXenContent = new ContentManager( Services, "Resources" );
            // TODO: Figure out a Silverlight solution for embedded content
            //_xenFonts = new SpriteFontCache( _embeddedXenContent, typeof( XenFonts ) );
#else
            _embeddedXenContent = new ResourceContentManager( Services, XenResources.ResourceManager );
            _xenFonts = new SpriteFontCache( _embeddedXenContent, typeof( XenFonts ) );
#endif

            foreach( IXenPlugin plugin in _plugins.Values )
            {
                plugin.Load();
            }
        }

        private void LoadPlugins()
        {
            // Check the plugin directory
#if !SILVERLIGHT // TODO: Find a solution for Silverlight and others
            string[] plugins = Directory.GetFiles( PluginDirectory, "*.dll" );
            for( int i = 0; i < plugins.Length; i++ )
            {
                try
                {
                    // Load the dll
                    Assembly assembly = Assembly.LoadFrom( plugins[ i ] );
                    if( assembly != null )
                    {
                        foreach( Type type in assembly.GetTypes() )
                        {
                            foreach( Type typeInt in type.GetInterfaces() )
                            {
                                if( typeInt == typeof( IXenPlugin ) )
                                {
                                    IXenPlugin xenPlugin = (IXenPlugin)Activator.CreateInstance( type );
                                    _plugins.Add( xenPlugin.Name, xenPlugin );
                                    xenPlugin.Initialize( _mainScene );
                                }
                            }
                        }
                    }
                }
                catch( Exception ex )
                {
                    // TODO: throw a more meaningful exception
                    throw ex;
                }
            }
#endif

            foreach( IXenPlugin plugin in _plugins.Values )
            {
                if( plugin.OnItemBeforeAdded != null )
                {
                    _mainScene.Children.OnItemBeforeAdded.Add( plugin.OnItemBeforeAdded );
                }
                if( plugin.OnItemAfterAdded != null )
                {
                    _mainScene.Children.OnItemAfterAdded.Add( plugin.OnItemAfterAdded );
                }
                if( plugin.OnItemBeforeRemoved != null )
                {
                    _mainScene.Children.OnItemBeforeRemoved.Add( plugin.OnItemBeforeRemoved );
                }
                if( plugin.OnItemAfterRemoved != null )
                {
                    _mainScene.Children.OnItemAfterRemoved.Add( plugin.OnItemAfterRemoved );
                }
            }
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            foreach( IXenPlugin plugin in _plugins.Values )
            {
                plugin.Unload();
            }
        }

        protected override void Update( GameTime gameTime )
        {
            _updatePerformanceTimer.Begin();
            base.Update( gameTime );
            bool firstUpdated = false;
            //long before = HeapUtility.GetTotalAllocatedMemory();

            Globals.LastUpdate = gameTime;

            if( _isFirstUpdate )
            {
                _gcTracker = new WeakReference( new Object() );
                _inputState = new InputState( Mouse.GetState(), Mouse.GetState(), Keyboard.GetState(), Keyboard.GetState() );
                _isFirstUpdate = false;
                firstUpdated = true;
            }
            else
            {
                _inputState = new InputState( Mouse.GetState(), _inputState.CurrentMouseState, Keyboard.GetState(), _inputState.CurrentKeyboardState );
            }

            ProcessInput( ref _inputState );

            if( !IsPaused )
            {
                UpdateBaseInternals( gameTime );
                ProcessBaseInput( ref _inputState );

                foreach( IXenPlugin plugin in _plugins.Values )
                {
                    plugin.Update( gameTime );
                }
            }

            if( !_gcTracker.IsAlive )
            {
                //Indicates a GC has occured.
                OnGarbageCollectionRun();
                _gcTracker = new WeakReference( new Object() );
            }

            long after = 0;
            //after = HeapUtility.GetTotalAllocatedMemory() - before;
            if( after != 0 )
            {
                if( firstUpdated )
                {
                    firstUpdated = false;
                }
                else
                {
                    Debugger.Break();
                }
            }
            _updatePerformanceTimer.End();
        }

        private void UpdateBaseInternals( GameTime gameTime )
        {
            MainScene.Update( gameTime );

            if( UseMouse )
            {
                XenMouse.Update( gameTime );
            }
        }

        private void ProcessBaseInput( ref InputState inputState )
        {
            MainScene.ProcessInput( ref inputState, Matrix.Identity );
        }

        protected virtual void ProcessInput( ref InputState input ) { }

        protected override void Draw( GameTime gameTime )
        {
            _drawPerformanceTimer.Begin();
            GraphicsDevice.Clear( BackgroundColor );

            _spriteBatch.Begin();

            DrawBaseInternals( gameTime );
            DrawInternal( gameTime );

            if( UseMouse )
            {
                //Order is important here.  Always draw the mouse last
                XenMouse.Draw( SpriteRenderer );
            }

            _spriteBatch.End();
            
            foreach( IXenPlugin plugin in _plugins.Values )
            {
                plugin.Draw( gameTime, SpriteRenderer );
            }
            _drawPerformanceTimer.End();

            // Performance stat refresh
            PerformanceTimer.FrameRefresh();
            _updatePerformanceTimer.Refresh();
            _drawPerformanceTimer.Refresh();

            if( ShowDebug )
            {
                // Render Framerate
                if( _xenFonts != null )
                {
                    SpriteRenderer.Begin();
                    SpriteRenderer.DrawString( _xenFonts[ (int)XenFonts.Arial ], XenString.Temporary( PerformanceTimer.AverageFramerate, 2 ), Vector2.UnitX * (Globals.Graphics.PreferredBackBufferWidth - 55), Color.White );
                    SpriteRenderer.End();
                }
            }

            PerformanceTimer.FrameTick();
        }

        private void DrawBaseInternals( GameTime gameTime )
        {
            MainScene.Draw( SpriteRenderer );
        }

        protected virtual void DrawInternal( GameTime gameTime ) { }
    }
}
