using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_CollisionSimple
{
    public class CollidableElement : Element2D<CollidableElement>
    {
        public static CollidableElement Acquire( Vector2 position, float rotation )
        {
            var instance = _pool.Acquire();
            instance._rotationBehavior.Reset( instance, rotation );
            instance.RenderingExtent.Anchor = position;
            return instance;
        }

        private StaticSprite _sprite;
        private RotationBehavior _rotationBehavior;

        public CollidableElement()
        {
            _sprite = StaticSprite.Acquire( Textures.Get( TexId.GrayRect ) );
            _rotationBehavior = RotationBehavior.Acquire();
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponent = _sprite;
            CollisionClass = CollisionClasses.Default;
            Behaviors.Add( _rotationBehavior );
        }
    }
}
