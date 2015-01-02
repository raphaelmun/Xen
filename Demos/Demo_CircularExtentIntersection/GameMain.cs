using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_CircularExtentIntersection
{
    /// <summary>
    /// This demo shows how to detect intersection between polygon extents.
    /// 
    /// Press and hold 1, 2, 3, or 4 to select one of the circles  
    /// Use W, A, S, D to move the circle
    /// Use Z, C to scale the circle in or out
    /// </summary>
    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        CircularExtent[] _circles = new CircularExtent[ 4 ];
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

            _circles[ 0 ] = new CircularExtent();
            _circles[ 0 ].Reset( new Vector2( 100, 100 ), 50 );
            _circles[ 0 ].ReAnchor( _circles[ 0 ].Center );

            _circles[ 1 ] = new CircularExtent();
            _circles[ 1 ].Reset( new Vector2( 300, 300 ), 50 );
            _circles[ 1 ].ReAnchor( _circles[ 1 ].Center );

            _circles[ 2 ] = new CircularExtent();
            _circles[ 2 ].Reset( new Vector2( 400, 350 ), 100  );
            _circles[ 2 ].ReAnchor( _circles[ 2 ].Center );

            _circles[ 3 ] = new CircularExtent();
            _circles[ 3 ].Reset( new Vector2( 300, 100 ), 75 );
            _circles[ 3 ].ReAnchor( _circles[ 3 ].Center );

            _contentLoaded = true;
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            #region Move Circle
            CircularExtent circleToMove = null;

            if( ks.IsKeyDown( Keys.D1 ) )
                circleToMove = _circles[ 0 ];
            if( ks.IsKeyDown( Keys.D2 ) )
                circleToMove = _circles[ 1 ];
            if( ks.IsKeyDown( Keys.D3 ) )
                circleToMove = _circles[ 2 ];
            if( ks.IsKeyDown( Keys.D4 ) )
                circleToMove = _circles[ 3 ];

            if( null != circleToMove )
            {
                if( ks.IsKeyDown( Keys.A ) )
                {
                    circleToMove.Anchor -= new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.D ) )
                {
                    circleToMove.Anchor += new Vector2( 5, 0 );
                }
                if( ks.IsKeyDown( Keys.W ) )
                {
                    circleToMove.Anchor -= new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.S ) )
                {
                    circleToMove.Anchor += new Vector2( 0, 5 );
                }
                if( ks.IsKeyDown( Keys.Q ) )
                {
                    circleToMove.Angle -= 0.04f;
                }
                if( ks.IsKeyDown( Keys.E ) )
                {
                    circleToMove.Angle += 0.04f;
                }
                if( ks.IsKeyDown( Keys.Z ) )
                {
                    circleToMove.Scale *= 1.04f;
                }
                if( ks.IsKeyDown( Keys.C ) )
                {
                    circleToMove.Scale /= 1.04f;
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
                    if( _circles[ i ].Intersects( _circles[ j ] ) )
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
                    _spriteBatch.DrawCircle( _circles[ i ].ActualCenter, _circles[ i ].Radius, drawColor );
                }
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
