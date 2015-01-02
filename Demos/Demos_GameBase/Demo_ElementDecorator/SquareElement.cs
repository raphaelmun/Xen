using XenGameBase;
using Xen2D;
using Microsoft.Xna.Framework;

namespace Demo_ElementDecorator
{
    public class SquareElement : Element2D<SquareElement>
    {
        public static SquareElement Acquire( Vector2 position )
        {
            var instance = _pool.Acquire();
            instance.RenderingExtent.ReAnchor( new Vector2( 100, 100 ) );
            instance.RenderingExtent.Anchor = position;
            return instance;
        }

        private StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.ColoredRect ), new Vector2( 100, 100 ) );

        public override void Reset()
        {
            base.Reset();
            VisualComponent = _sprite;
            Behaviors.Add( KeyboardMoveBehavior.Acquire( this, 5 ) );

            Decorators.Add( StaticSprite.AcquireAndCenter( Textures.Get( TexId.Marker_Red ) ), _sprite.RenderingExtent.ReferenceTopLeft );
            Decorators.Add( StaticSprite.AcquireAndCenter( Textures.Get( TexId.Marker_Red ) ), _sprite.RenderingExtent.ReferenceTopRight );
            Decorators.Add( StaticSprite.AcquireAndCenter( Textures.Get( TexId.Marker_Red ) ), _sprite.RenderingExtent.ReferenceBottomLeft );
            Decorators.Add( StaticSprite.AcquireAndCenter( Textures.Get( TexId.Marker_Red ) ), _sprite.RenderingExtent.ReferenceBottomRight );

            var topLeft = XenString.Acquire();
            topLeft.Reset( Fonts.Get( FontId.Arial ), "Top Left" );
            Decorators.Add( topLeft, _sprite.RenderingExtent.ReferenceTopLeft );

            var topRight= XenString.Acquire();
            topRight.Reset( Fonts.Get( FontId.Arial ), "Top Right" );
            Decorators.Add( topRight, _sprite.RenderingExtent.ReferenceTopRight );

            var bottomLeft = XenString.Acquire();
            bottomLeft.Reset( Fonts.Get( FontId.Arial ), "Bottom Left" );
            Decorators.Add( bottomLeft, _sprite.RenderingExtent.ReferenceBottomLeft );

            var bottomRight = XenString.Acquire();
            bottomRight.Reset( Fonts.Get( FontId.Arial ), "Bottom Right" );
            Decorators.Add( bottomRight, _sprite.RenderingExtent.ReferenceBottomRight );
        }
    }
}
