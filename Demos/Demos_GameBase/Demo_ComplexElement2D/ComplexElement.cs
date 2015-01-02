using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_ComplexElement2D
{
    public class ComplexElement : ComplexElement2D<ComplexElement>
    {
        RenderParams _renderParamsTemplate = RenderParams.Default;

        private StaticSprite _coloredRect = StaticSprite.Acquire( Textures.Get( TexId.ColoredRect ) );
        private StaticSprite _grayRect = StaticSprite.Acquire( Textures.Get( TexId.GrayRect ) );
        private CircularExtent _collisionExtent = CircularExtent.Acquire( new Vector2( 85, 85 ), 25 );
        private KeyboardMoveBehavior _moveBehavior;

        StaticSprite _markerAnchor_Blue;
        StaticSprite _markerTopLeft_Red;
        StaticSprite _markerCompositeAnchor_Green;

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public ComplexElement()
        {
            _moveBehavior = KeyboardMoveBehavior.Acquire( this, 5.0f );

            _renderParamsTemplate.Mode = RenderMode.TraceBoundingBox | RenderMode.TraceLogicalExtent | RenderMode.TraceRenderingExtent;

            _markerAnchor_Blue = StaticSprite.Acquire( Textures.Get( TexId.Marker_Blue ), new Vector2( 4, 4 ) );
            _markerAnchor_Blue.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkCenter = new Getter<ISprite>( () => { return _markerAnchor_Blue; } );

            _markerTopLeft_Red = StaticSprite.Acquire( Textures.Get( TexId.Marker_Red ), new Vector2( 4, 4 ) );
            _markerTopLeft_Red.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkOrigin = new Getter<ISprite>( () => { return _markerTopLeft_Red; } );

            _markerCompositeAnchor_Green = StaticSprite.Acquire( Textures.Get( TexId.Marker_Green ), new Vector2( 4, 4 ) );
            _markerCompositeAnchor_Green.LayerDepth = 0;
            _renderParamsTemplate.GetTexture_MarkTopLeft = new Getter<ISprite>( () => { return _markerCompositeAnchor_Green; } );
        }

        public override void Reset()
        {
            base.Reset();
            Behaviors.Add( _moveBehavior );
            VisualComponents.AddExtent( _collisionExtent );
            VisualComponents.Add( _coloredRect );
            _grayRect.RenderingExtent.Anchor += new Vector2( 200, 200 );
            VisualComponents.Add( _grayRect );
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            //_spriteBatch.DrawSprite( _child1, worldToCamera, _renderParamsTemplate );
            //_spriteBatch.DrawSprite( _child2, worldToCamera, _renderParamsTemplate );
            //_spriteBatch.DrawPolygonExtent( _compositeExtent, worldToCamera, _renderParamsTemplate );
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawExtent( CollisionExtent, Color.LimeGreen, transformFromWorldToCamera );
            spriteBatch.DrawPolygonExtent( _grayRect.RenderingExtent, transformFromWorldToCamera, _renderParamsTemplate );
            spriteBatch.DrawPolygonExtent( _coloredRect.RenderingExtent, transformFromWorldToCamera, _renderParamsTemplate );
        }
    }
}
