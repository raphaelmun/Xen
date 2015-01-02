using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace Demo_CompositeExtent
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

    public enum Textures : int
    {
        [ContentIdentifier( "textures\\colored_rect_200x200" )]
        ColoredRect,
        [ContentIdentifier( "textures\\explosion0" )]
        Explosion,
        [ContentIdentifier( "textures\\marker_blue" )]
        Marker_Blue,
        [ContentIdentifier( "textures\\marker_green" )]
        Marker_Green,
        [ContentIdentifier( "textures\\marker_red" )]
        Marker_Red,
        [ContentIdentifier( "textures\\gray_rect_100x200" )]
        GrayRect,
    }

    /// <summary>
    /// This demo shows how composite extents can be used to apply a transform uniformly to a set of sprites.  
    /// Press W,A,S,D,Q,E,Z,C to transform the composite
    /// Hold down 1 and Press W,A,S,D,Q,E,Z,C to transform the colored square
    /// Hold down 2 and Press W,A,S,D,Q,E,Z,C to transform the gray rectangle
    /// Press Insert to toggle whether the texture is drawn
    /// Press Home to toggle whether the axis-aligned bounding box is drawn
    /// Press PageUp to toggle whether extent tracing and key points
    /// 
    /// Keypoints legend:
    /// Green- Top Left
    /// Red- Origin
    /// Blue- Center
    /// 
    /// NOTE: The composite extent has all three keypoints collapsed into a single point, and thus will display only a green point
    /// </summary>
    public class GameMain : Game
    {
        Texture2DCache _textures;
        SpriteBatch _spriteBatch;
        RenderParams _renderParamsTemplate = RenderParams.Default;
        KeyboardState _previousKeyboardState;

        StaticSprite _child1;
        StaticSprite _child2;
        StaticSprite _markerAnchor_Blue;
        StaticSprite _markerTopLeft_Red;
        StaticSprite _markerCompositeAnchor_Green;
        CompositeExtent _compositeExtent;

        bool _contentLoaded = false;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        protected override void LoadContent()
        {
            _textures = new Texture2DCache( typeof( Textures ) );

            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );
            _child1 = StaticSprite.Acquire( _textures[ (int)Textures.ColoredRect ], new Vector2( 100, 100 ) );
            _child1.LayerDepth = 1;

            _child2 = StaticSprite.Acquire( _textures[ (int)Textures.GrayRect ], new Vector2( 25, 50 ) );
            _child2.RenderingExtent.Anchor = new Vector2( 250, 250 );
            _child2.LayerDepth = 1;

            _markerAnchor_Blue = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Blue ], new Vector2( 4, 4 ) );
            _markerAnchor_Blue.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkCenter = new Getter<ISprite>( () => { return _markerAnchor_Blue; } );

            _markerTopLeft_Red = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Red ], new Vector2( 4, 4 ) );
            _markerTopLeft_Red.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkOrigin = new Getter<ISprite>( () => { return _markerTopLeft_Red; } );

            _markerCompositeAnchor_Green = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Green ], new Vector2( 4, 4 ) );
            _markerCompositeAnchor_Green.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkTopLeft = new Getter<ISprite>( () => { return _markerCompositeAnchor_Green; } );

            _compositeExtent = new CompositeExtent();
            _compositeExtent.Add( _child1.RenderingExtent );
            _compositeExtent.Add( _child2.RenderingExtent );

            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            if( ks.IsKeyDown( Keys.Escape ) )
            {
                this.Exit();
            }
            else if( ks.IsKeyDown( Keys.D1 ) )
            {
                #region Child1
                if( ks.IsKeyDown( Keys.A ) )
                {
                    _child1.RenderingExtent.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    _child1.RenderingExtent.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    _child1.RenderingExtent.Anchor += new Vector2( 0, -5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    _child1.RenderingExtent.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    _child1.RenderingExtent.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    _child1.RenderingExtent.Angle += 0.04f;
                }
                if( ks.IsKeyDown( Keys.Z ) )
                {
                    _child1.RenderingExtent.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    _child1.RenderingExtent.Scale /= 1.04f;
                }
                #endregion
            }
            else if( ks.IsKeyDown( Keys.D2 ) )
            {
                #region Child2
                if( ks.IsKeyDown( Keys.A ) )
                {
                    _child2.RenderingExtent.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    _child2.RenderingExtent.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    _child2.RenderingExtent.Anchor += new Vector2( 0, -5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    _child2.RenderingExtent.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    _child2.RenderingExtent.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    _child2.RenderingExtent.Angle += 0.04f;
                }
                if( ks.IsKeyDown( Keys.Z ) )
                {
                    _child2.RenderingExtent.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    _child2.RenderingExtent.Scale /= 1.04f;
                }
                #endregion
            }
            else
            {
                #region Composite
                if( ks.IsKeyDown( Keys.A ) )
                {
                    _compositeExtent.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    _compositeExtent.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    _compositeExtent.Anchor += new Vector2( 0, -5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    _compositeExtent.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    _compositeExtent.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    _compositeExtent.Angle += 0.04f;
                }

                if( ks.IsKeyDown( Keys.Z ) )
                {
                    _compositeExtent.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    _compositeExtent.Scale /= 1.04f;
                }
                #endregion
            }

            if( _previousKeyboardState.IsKeyDown( Keys.Insert ) && ks.IsKeyUp( Keys.Insert ) )
                _renderParamsTemplate.Mode ^= RenderMode.Texture;
            if( _previousKeyboardState.IsKeyDown( Keys.Home ) && ks.IsKeyUp( Keys.Home ) )
                _renderParamsTemplate.Mode ^= RenderMode.TraceBoundingBox;
            if( _previousKeyboardState.IsKeyDown( Keys.PageUp ) && ks.IsKeyUp( Keys.PageUp ) )
                _renderParamsTemplate.Mode ^= RenderMode.TraceRenderingExtent;

            _previousKeyboardState = ks;
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            Matrix worldToCamera = Matrix.Identity;

            _spriteBatch.Begin();

            if( _contentLoaded )
            {
                _spriteBatch.DrawSprite( _child1, worldToCamera, _renderParamsTemplate );
                _spriteBatch.DrawSprite( _child2, worldToCamera, _renderParamsTemplate );
                _spriteBatch.DrawPolygonExtent( _compositeExtent, worldToCamera, _renderParamsTemplate );
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
