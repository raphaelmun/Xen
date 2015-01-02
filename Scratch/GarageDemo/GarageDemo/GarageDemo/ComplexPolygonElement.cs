using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using Xen2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GarageDemo
{
    public class ComplexPolygonElement : ComplexElement2D<ComplexPolygonElement>
    {
        StaticSprite _markerIntersect_Blue;
        StaticSprite _markerPoint_Red;
        IPolygonExtent _polygonExtent;

        private KeyboardMoveBehavior _moveBehavior;
        private TouchManipulationBehavior _touchBehavior;

        float _yValue = 185;//105;
        float _xValue = 450;//204;
        XenString _insidePolygon;

        public ComplexPolygonElement()
        {
            _moveBehavior = KeyboardMoveBehavior.Acquire( this, 5.0f );
#if SURFACE
            _touchBehavior = TouchManipulationBehavior.Acquire( this, SurfaceGameBase.ContactTarget );
#else
#if !SILVERLIGHT
            _touchBehavior = TouchManipulationBehavior.Acquire( this );
#endif
#endif
            _markerIntersect_Blue = StaticSprite.Acquire( Textures.Get( TexId.Marker_Blue ), new Vector2( 4, 4 ) );
            _markerIntersect_Blue.LayerDepth = 0;

            _markerPoint_Red = StaticSprite.Acquire( Textures.Get( TexId.Marker_Red ), new Vector2( 4, 4 ) );
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

            _polygonExtent = new PolygonExtent();
            _polygonExtent.Reset( vertices );

            _insidePolygon = XenString.Acquire();
            _insidePolygon.Reset( Fonts.Get( FontId.Arial ), "Inside!" );
            _insidePolygon.RenderingExtent.Anchor = new Vector2( _xValue, _yValue - 25.0f );
        }

        public override void Reset()
        {
            base.Reset();
            Behaviors.Add( _moveBehavior );
            Behaviors.Add( _touchBehavior );
            VisualComponents.AddExtent( _polygonExtent );
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );

            spriteBatch.DrawPolygon( _polygonExtent, Color.Black );
            spriteBatch.DrawExtent( CollisionExtent, Color.LimeGreen, transformFromWorldToCamera );
            //spriteBatch.DrawPolygonExtent( _polygonExtent, RenderParams.Debug );

            for( int i = 0; i < _polygonExtent.NumSides; i++ )
            {
                int nextIndex = ( i + 1 ) % _polygonExtent.NumSides;
                Vector2? intersection = ShapeUtility.FindYIntersectionPoint( _polygonExtent.Vertices[ i ], _polygonExtent.Vertices[ nextIndex ], _yValue );
                if( intersection != null )
                {
                    _markerIntersect_Blue.RenderingExtent.Anchor = intersection.Value;
                    spriteBatch.DrawSprite( _markerIntersect_Blue );
                }
            }

            spriteBatch.DrawSprite( _markerPoint_Red );

            if( _polygonExtent.ContainsPoint( new Vector2( _xValue, _yValue ) ) )
            {
                spriteBatch.DrawString( _insidePolygon );
            }
        }
    }
}
