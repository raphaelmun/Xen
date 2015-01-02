using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_BasicScene
{
    public class BasicElement : Element2D<BasicElement>
    {
        private static StaticSprite AcquireSprite()
        {
            return StaticSprite.Acquire( Textures.Get( TexId.RectBlueHorizontal ) );
        }

        ComposedField<StaticSprite> _sprite = new ComposedField<StaticSprite>( AcquireSprite );
        StaticSprite Sprite { get { return _sprite.Value; } }

        public BasicElement()
        {
            DeclareComposition( _sprite );
        }

        protected override void ResetDirectState()
        {
            VisualComponent = _sprite.Value;
            base.ResetDirectState();
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            Sprite.Draw( spriteBatch, transformFromWorldToCamera );
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
        }

        public override IRectangularExtent RenderingExtent
        {
            get { return Sprite.RenderingExtent; }
        }
    }
}
