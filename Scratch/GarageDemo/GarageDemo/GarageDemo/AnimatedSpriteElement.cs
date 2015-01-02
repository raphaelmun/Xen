using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace GarageDemo
{
    public class AnimatedSpriteElement : Element2D<AnimatedSpriteElement>
    {
        AnimatedSprite _sprite;

        public AnimatedSpriteElement()
        {
            _sprite = AnimatedSprite.Acquire();
            _sprite.ResetFromCenter( Textures.Get( TexId.Explosion ), 5, 5, new Vector2( 100, 100 ) );
            _sprite.RenderingExtent.Scale = Vector2.One * 1.5f;
            VisualComponent = _sprite;
        }

        public override void Reset()
        {
            base.Reset();
#if SURFACE
            Behaviors.Add( SetAnchorPositionBehavior.Acquire( this, SurfaceGameBase.ContactTarget ) );
#else
            Behaviors.Add( SetAnchorPositionBehavior.Acquire( this ) );
#endif
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            _sprite.Update( gameTime );
            base.UpdateInternal( gameTime );
        }
    }
}
