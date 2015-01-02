using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Demo_FarseerPlugin
{
    class SquareElement : ComplexElement2D<SquareElement>
    {
        public static SquareElement Acquire( Vector2 position )
        {
            var instance = _pool.Acquire();
            instance.RenderingExtent.Anchor = position;
            return instance;
        }

        private StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.Square ) );

        public override void Reset()
        {
            //Note: always set Visual Component, then base.ResetDirectState()
            VisualComponents.Add( _sprite );
            base.Reset();

            CollisionClass = CollisionClasses.Default;
        }
    }
}
