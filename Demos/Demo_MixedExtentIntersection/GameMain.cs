using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_MixedExtentIntersection
{
    /// <summary>
    /// This demo shows how to detect intersection between extents of mixed types (circles and polygons)
    /// 
    /// Press and hold 1, 2, 3, or 4 to select one of the extents  
    /// Use W, A, S, D to move the extent
    /// Use Z, C to scale the selected extent in or out
    /// </summary>
    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        IExtent[] _extents = new IExtent[ 5 ];
        bool[] _inCollision = new bool[ 5 ];
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

            var circle = new CircularExtent();
            circle.Reset( new Vector2( 100, 100 ), 50 );
            circle.ReAnchor( circle.Center );
            _extents[ 0 ] = circle;

            circle = new CircularExtent();
            circle.Reset( new Vector2( 300, 300 ), 75 );
            circle.ReAnchor( circle.Center );
            _extents[ 1 ] = circle;

            var vertices = new List<Vector2>();

            vertices.Add( new Vector2( 300, 300 ) );
            vertices.Add( new Vector2( 400, 300 ) );
            vertices.Add( new Vector2( 420, 400 ) );
            vertices.Add( new Vector2( 350, 450 ) );
            vertices.Add( new Vector2( 280, 400 ) );

            var poly = new PolygonExtent();
            poly.Reset( vertices );
            poly.ReAnchor( poly.Center );
            _extents[ 2 ] = poly;

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

            poly = new PolygonExtent();
            poly.Reset( vertices );
            poly.ReAnchor( poly.Center );
            _extents[ 3 ] = poly;

            var composite = new CompositeExtent();

            var child1 = new CircularExtent();
            child1.Reset( new Vector2( 50, 50 ), 50 );
            child1.ReAnchor( circle.Center );

            vertices = new List<Vector2>();

            vertices.Add( new Vector2( 100, 100 ) );
            vertices.Add( new Vector2( 200, 100 ) );
            vertices.Add( new Vector2( 200, 200 ) );
            vertices.Add( new Vector2( 100, 200 ) );

            var child2 = new PolygonExtent();
            child2.Reset( vertices );
            child2.ReAnchor( poly.Center );

            composite.Add( child1 );
            composite.Add( child2 );

            _extents[ 4 ] = composite;
            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            #region Move Extent
            IExtent extentToMove = null;

            if( ks.IsKeyDown( Keys.D1 ) )
                extentToMove = _extents[ 0 ];
            if( ks.IsKeyDown( Keys.D2 ) )
                extentToMove = _extents[ 1 ];
            if( ks.IsKeyDown( Keys.D3 ) )
                extentToMove = _extents[ 2 ];
            if( ks.IsKeyDown( Keys.D4 ) )
                extentToMove = _extents[ 3 ];
            if( ks.IsKeyDown( Keys.D5 ) )
                extentToMove = _extents[ 4 ];

            if( null != extentToMove )
            {
                if( ks.IsKeyDown( Keys.A ) )
                {
                    extentToMove.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    extentToMove.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    extentToMove.Anchor -= new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    extentToMove.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    extentToMove.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    extentToMove.Angle += 0.04f;
                }
                if( ks.IsKeyDown( Keys.Z ) )
                {
                    extentToMove.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    extentToMove.Scale /= 1.04f;
                }
            }
            #endregion

            #region Check for Collisions
            for( int i = 0; i < 5; i++ )
            {
                _inCollision[ i ] = false;
            }

            for( int i = 0; i < 5; i++ )
            {
                for( int j = i + 1; j < 5; j++ )
                {
                    if( _extents[ i ].Intersects( _extents[ j ] ) )
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
                for( int i = 0; i < 5; i++ )
                {
                    Color drawColor = _inCollision[ i ] ? Color.Red : Color.Blue;
                    _spriteBatch.DrawExtent( _extents[ i ], drawColor );
                }
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
