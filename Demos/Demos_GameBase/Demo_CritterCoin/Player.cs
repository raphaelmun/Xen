using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace Demo_CritterCoin
{
    public class ComplexPlayer : ComplexElement2D<ComplexPlayer>
    {
        private StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.Critter ) );
        private CircularExtent _collisionExtent = CircularExtent.Acquire( new Vector2( 85, 85 ), 25 );
        private KeyboardMoveBehavior _moveBehavior;

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public ComplexPlayer()
        {
            _moveBehavior = KeyboardMoveBehavior.Acquire( this, 5.0f );
        }

        public override void Reset()
        {
            base.Reset();
            CollisionClass = (uint)CustomCollisionClasses.Player;
            Behaviors.Add( _moveBehavior );
            VisualComponents.AddExtent( _collisionExtent );
            VisualComponents.Add( _sprite );
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawExtent( CollisionExtent, Color.GreenYellow );
        }
    }

    public class SimplePlayer : Element2D<SimplePlayer>
    {
        private KeyboardMoveBehavior _moveBehavior;

        public SimplePlayer()
        {
            CollisionClass = (uint)CustomCollisionClasses.Player;
            _moveBehavior = KeyboardMoveBehavior.Acquire( this, 5.0f );
            VisualComponent = StaticSprite.Acquire( Textures.Get( TexId.Critter ) );
        }

        public override void Reset()
        {
            base.Reset();
            Behaviors.Add( _moveBehavior );
        }
    }
}
