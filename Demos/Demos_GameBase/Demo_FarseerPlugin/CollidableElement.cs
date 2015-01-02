using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_FarseerPlugin
{
    class CollidableElement : ComplexElement2D<CollidableElement>
    {
        public static CollidableElement Acquire( Vector2 position )
        {
            var instance = _pool.Acquire();
            instance.RenderingExtent.Anchor = position;
            return instance;
        }

        private StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.GrayRect ) );

        protected override void ResetDirectState()
        {
            //Note: always set Visual Component, then base.ResetDirectState()
            VisualComponents.Add( _sprite );
            base.ResetDirectState();

            CollisionClass = CollisionClasses.Default;
        }
    }
}
