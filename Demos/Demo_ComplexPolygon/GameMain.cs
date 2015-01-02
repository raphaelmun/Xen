using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace Demo_ComplexPolygon
{
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
        Marker_Red
    }

    public enum Fonts : int
    {
        [ContentIdentifier( "Arial" )]
        Arial
    }

    public class GameMain : Game
    {
        SpriteBatch _spriteBatch;
        SpriteFontCache _fonts;
        StaticSprite _markerIntersect_Blue;
        StaticSprite _markerPoint_Red;
        Texture2DCache _textures;
        IPolygonExtent _polygonExtent;

        float _yValue = 105;
        float _xValue = 204;
        XenString _insidePolygon;

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
            _fonts = new SpriteFontCache( typeof( Fonts ) );

            _markerIntersect_Blue = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Blue ], new Vector2( 4, 4 ) );
            _markerIntersect_Blue.LayerDepth = 0;

            _markerPoint_Red = StaticSprite.Acquire( _textures[ (int)Textures.Marker_Red ], new Vector2( 4, 4 ) );
            _markerPoint_Red.LayerDepth = 0;
            _markerPoint_Red.RenderingExtent.Anchor = new Vector2( _xValue, _yValue );
            
            var vertices = new List<Vector2>();
            vertices.Add( new Vector2( 22, 122 ) );
            vertices.Add( new Vector2( 66, 23 ) );
            vertices.Add( new Vector2( 150, 5 ) );
            vertices.Add( new Vector2( 293, 78 ) );
            vertices.Add( new Vector2( 256, 194 ) );
            vertices.Add( new Vector2( 230, 86 ) );
            vertices.Add( new Vector2( 202, 175 ) );
            vertices.Add( new Vector2( 113, 157 ) );
            vertices.Add( new Vector2( 168, 69 ) );
            vertices.Add( new Vector2( 165, 148 ) );
            vertices.Add( new Vector2( 203, 60 ) );
            vertices.Add( new Vector2( 105, 50 ) );
            vertices.Add( new Vector2( 77, 140 ) );
            vertices.Add( new Vector2( 40, 147 ) );

            _insidePolygon = XenString.Acquire();
            _insidePolygon.Reset( _fonts[ (int)Fonts.Arial], "Inside!" );

            _polygonExtent = new PolygonExtent();
            _polygonExtent.Reset( vertices );
        }

        protected override void UnloadContent()
        {
            _markerIntersect_Blue.Release();
            _markerPoint_Red.Release();
            base.UnloadContent();
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            KeyboardState ks = Keyboard.GetState();

            if( ks.IsKeyDown( Keys.Left ) )
            {
                _xValue -= 200 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if( ks.IsKeyDown( Keys.Right ) )
            {
                _xValue += 200 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if( ks.IsKeyDown( Keys.Up ) )
            {
                _yValue -= 200 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if( ks.IsKeyDown( Keys.Down ) )
            {
                _yValue += 200 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            _markerPoint_Red.RenderingExtent.Anchor = new Vector2( _xValue, _yValue );

            #region Apply changes to the extent.
            if( ks.IsKeyDown( Keys.A ) )
            {
                _polygonExtent.Anchor -= new Vector2( 5, 0 );
            }
            if( ks.IsKeyDown( Keys.D ) )
            {
                _polygonExtent.Anchor += new Vector2( 5, 0 );
            }
            if( ks.IsKeyDown( Keys.W ) )
            {
                _polygonExtent.Anchor += new Vector2( 0, -5 );
            }
            if( ks.IsKeyDown( Keys.S ) )
            {
                _polygonExtent.Anchor += new Vector2( 0, 5 );
            }
            if( ks.IsKeyDown( Keys.Q ) )
            {
                _polygonExtent.Angle -= 0.04f;
            }
            if( ks.IsKeyDown( Keys.E ) )
            {
                _polygonExtent.Angle += 0.04f;
            }

            if( ks.IsKeyDown( Keys.Z ) )
            {
                _polygonExtent.Scale *= 1.04f;
            }
            if( ks.IsKeyDown( Keys.C ) )
            {
                _polygonExtent.Scale /= 1.04f;
            }

            if( ks.IsKeyDown( Keys.F ) )
            {
                _polygonExtent.Scale = new Vector2( _polygonExtent.Scale.X, _polygonExtent.Scale.Y * 1.04f );
            }
            if( ks.IsKeyDown( Keys.G ) )
            {
                _polygonExtent.Scale = new Vector2( _polygonExtent.Scale.X, _polygonExtent.Scale.Y / 1.04f );
            }
            if( ks.IsKeyDown( Keys.V ) )
            {
                _polygonExtent.Scale = new Vector2( _polygonExtent.Scale.X * 1.04f, _polygonExtent.Scale.Y );
            }
            if( ks.IsKeyDown( Keys.B ) )
            {
                _polygonExtent.Scale = new Vector2( _polygonExtent.Scale.X / 1.04f, _polygonExtent.Scale.Y );
            }
            #endregion

            base.Update( gameTime );
        }
        
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            _spriteBatch.Begin();

            _spriteBatch.DrawPolygon( _polygonExtent, Color.Black );
            _spriteBatch.DrawPolygonExtent( _polygonExtent, RenderParams.Debug );

            for( int i = 0; i < _polygonExtent.NumSides; i++ )
            {
                int nextIndex = ( i + 1 ) % _polygonExtent.NumSides;
                Vector2? intersection = ShapeUtility.FindYIntersectionPoint( _polygonExtent.Vertices[ i ], _polygonExtent.Vertices[ nextIndex ], _yValue );
                if( intersection != null )
                {
                    _markerIntersect_Blue.RenderingExtent.Anchor = intersection.Value;
                    _spriteBatch.DrawSprite( _markerIntersect_Blue );
                }
            }

            _spriteBatch.DrawSprite( _markerPoint_Red );

            if( _polygonExtent.ContainsPoint( new Vector2( _xValue, _yValue ) ) )
            {
                _spriteBatch.DrawString( _insidePolygon );
            }

            _spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
