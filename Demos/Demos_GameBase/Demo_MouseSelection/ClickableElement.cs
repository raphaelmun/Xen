using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_MouseSelection
{
    class ClickableElement : ComplexElement2D<ClickableElement>
    {
        private StaticSprite _sprite1;
        private StaticSprite _sprite2;

        private OutlineBehavior _outlineBehavior;
        private RotationBehavior _rotationBehavior;
        private FlashingBehavior _flashingBehavior;

        public ClickableElement()
        {
            _sprite1 = StaticSprite.Acquire( Textures.Get( TexId.Square ) );
            _sprite2 = StaticSprite.Acquire( Textures.Get( TexId.Square ) );
            _sprite2.RenderingExtent.Anchor += _sprite1.RenderingExtent.ActualBottomRight;
            
            _outlineBehavior = OutlineBehavior.Acquire();
            _outlineBehavior.Reset( this, Color.LimeGreen );

            _rotationBehavior = RotationBehavior.Acquire();
            _rotationBehavior.Reset( this, 0.02f );

            _flashingBehavior = FlashingBehavior.Acquire();
            _flashingBehavior.Reset( this, 0.5f );
        }

        public override void Reset()
        {
            base.Reset();
            Behaviors.Add( _outlineBehavior );
            Behaviors.Add( _rotationBehavior );
            Behaviors.Add( _flashingBehavior );

            VisualComponents.Add( _sprite1 );
            VisualComponents.Add( _sprite2 );

            RenderingExtent.ReAnchor( _sprite1.RenderingExtent.ActualBottomRight );
        }
    }
}
