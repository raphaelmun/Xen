using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_PolygonIntersection
{
    /// <summary>
    /// This demo shows how to detect intersection between polygon extents.
    /// 
    /// Press and hold 1, 2, 3, or 4 to select one of the polygons.  
    /// Use W,A,S,D to move the selected polygon.
    /// Use Z, C to scale the selected polygon.
    /// Use Q and E to rotate the selected polygon.  
    /// </summary>
    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        PolygonExtent[] _polygons = new PolygonExtent[ 4 ];
        bool[] _inCollision = new bool[ 4 ];
        bool _contentLoaded = false;

        public GameMain()
        {
            Globals.Graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            Globals.Content = Content;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch( Globals.GraphicsDevice );

            ScreenUtility.InitGraphicsMode( 800, 480, false );

            var vertices = new List<Vector2>();
            vertices.Add( new Vector2( 100, 100 ) );
            vertices.Add( new Vector2( 200, 100 ) );
            vertices.Add( new Vector2( 150, 150 ) );

            _polygons[ 0 ] = new PolygonExtent();
            _polygons[ 0 ].Reset( vertices );
            _polygons[ 0 ].ReAnchor( _polygons[ 0 ].Center );

            vertices.Clear();
            vertices.Add( new Vector2( 300, 300 ) );
            vertices.Add( new Vector2( 400, 300 ) );
            vertices.Add( new Vector2( 400, 400 ) );
            vertices.Add( new Vector2( 300, 400 ) );

            _polygons[ 1 ] = new PolygonExtent();
            _polygons[ 1 ].Reset( vertices );
            _polygons[ 1 ].ReAnchor( _polygons[ 1 ].Center );

            vertices.Clear();
            vertices.Add( new Vector2( 300, 300 ) );
            vertices.Add( new Vector2( 400, 300 ) );
            vertices.Add( new Vector2( 420, 400 ) );
            vertices.Add( new Vector2( 350, 450 ) );
            vertices.Add( new Vector2( 280, 400 ) );

            _polygons[ 2 ] = new PolygonExtent();
            _polygons[ 2 ].Reset( vertices );
            _polygons[ 2 ].ReAnchor( _polygons[ 2 ].Center );

            vertices.Clear();
            Vector2 offset = new Vector2( 300, 100 );
            vertices.Add( offset + new Vector2( 0, 0 ) );
            vertices.Add( offset + new Vector2( 50, 0 ) );
            vertices.Add( offset + new Vector2( 50, -50 ) );
            vertices.Add( offset + new Vector2( 100, -50 ) );
            vertices.Add( offset + new Vector2( 100, 0 ) );
            vertices.Add( offset + new Vector2( 150, 0 ) );
            vertices.Add( offset + new Vector2( 150, 50 ) );
            vertices.Add( offset + new Vector2( 100, 50 ) );
            vertices.Add( offset + new Vector2( 100, 100 ) );
            vertices.Add( offset + new Vector2( 50, 100 ) );
            vertices.Add( offset + new Vector2( 50, 50 ) );
            vertices.Add( offset + new Vector2( 0, 50 ) );

            _polygons[ 3 ] = new PolygonExtent();
            _polygons[ 3 ].Reset( vertices );
            _polygons[ 3 ].ReAnchor( _polygons[ 3 ].Center );

            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            #region Move Polygon
            PolygonExtent polygonToMove = null;

            if( ks.IsKeyDown( Keys.D1 ) )
                polygonToMove = _polygons[ 0 ];
            if( ks.IsKeyDown( Keys.D2 ) )
                polygonToMove = _polygons[ 1 ];
            if( ks.IsKeyDown( Keys.D3 ) )
                polygonToMove = _polygons[ 2 ];
            if( ks.IsKeyDown( Keys.D4 ) )
                polygonToMove = _polygons[ 3 ];

            if( null != polygonToMove )
            {
                if( ks.IsKeyDown( Keys.A ) )
                {
                    polygonToMove.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    polygonToMove.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    polygonToMove.Anchor -= new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    polygonToMove.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    polygonToMove.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    polygonToMove.Angle += 0.04f;
                }
                if( ks.IsKeyDown( Keys.Z ) )
                {
                    polygonToMove.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    polygonToMove.Scale /= 1.04f;
                }
            }
            #endregion

            #region Check for Collisions
            for( int i = 0; i < 4; i++ )
            {
                _inCollision[ i ] = false;
            }

            for( int i = 0; i < 4; i++ )
            {
                if( i == 3 )
                    break;

                for( int j = i + 1; j < 4; j++ )
                {
                    if( _polygons[ i ].Intersects( _polygons[ j ] ) )
                    {
                        _inCollision[ i ] = true;
                        _inCollision[ j ] = true;
                    }
                }
            }
            #endregion
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            if( _contentLoaded )
            {
                for( int i = 0; i < 4; i++ )
                {
                    Color drawColor = _inCollision[ i ] ? Color.Red : Color.Blue;
                    _spriteBatch.DrawPolygon( _polygons[ i ], drawColor );
                }
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
