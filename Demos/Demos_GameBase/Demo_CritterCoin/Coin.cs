using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_CritterCoin
{
    public class ComplexCoin : ComplexElement2D<ComplexCoin>
    {
        private StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.Coin ) );
        private CircularExtent _collisionExtent = CircularExtent.Acquire( new Vector2( 32, 32 ), 20 );

        public override void Reset()
        {
            base.Reset();
            CollisionClass = (uint)CustomCollisionClasses.Coin;
            CollisionMode = CollisionMode.InnerCenterCircle;
            VisualComponents.AddExtent( _collisionExtent );
            VisualComponents.Add( _sprite );
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawExtent( CollisionExtent, Color.GreenYellow );
        }

        public override IExtent CollisionExtent { get { return _collisionExtent; } }
    }

    public class SimpleCoin : Element2D<SimpleCoin>
    {
        private StaticSprite _visual = StaticSprite.Acquire( Textures.Get( TexId.Coin ) );

        protected override void ResetDirectState()
        {
 	        base.ResetDirectState();
            CollisionClass = (uint)CustomCollisionClasses.Coin;
            CollisionMode = CollisionMode.InnerCenterCircle;
            VisualComponent = _visual;
        }
    }
}
