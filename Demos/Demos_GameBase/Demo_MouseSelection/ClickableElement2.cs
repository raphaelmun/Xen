using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_MouseSelection
{
    class ClickableElement2 : Element2D<ClickableElement2>
    {
        private StaticSprite _sprite;
        private OutlineBehavior _outlineBehavior;
        private RotationBehavior _rotationBehavior;

        public ClickableElement2()
        {
            _sprite = StaticSprite.Acquire( Textures.Get( TexId.GrayRect ) );
            
            _outlineBehavior = OutlineBehavior.Acquire();
            _outlineBehavior.Reset( this, Color.Magenta );

            _rotationBehavior = RotationBehavior.Acquire();
            _rotationBehavior.Reset( this, -0.01f );
        }

        public override void Reset()
        {
            VisualComponent = _sprite;
            base.Reset();
            Behaviors.Add( _outlineBehavior );
            Behaviors.Add( _rotationBehavior );
        }
    }
}
