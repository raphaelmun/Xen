using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    public class Laser : Element2D<Laser>
    {
        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponent = StaticSprite.Acquire( Textures.Get( TexId.Laser ) );
            CollisionClass = (uint)CustomCollisionClass.Projectile;
            RenderingExtent.Anchor = Vector2.Zero;
            RenderingExtent.ReAnchor( new Vector2( 0, 5 ) );
            Behaviors.Add( MoveForwardBehavior.Acquire( this, 25 ) );
            Behaviors.Add( ReleasesAfterTimeBehavior.Acquire( this, 1.5f, (float)Globals.LastUpdate.TotalGameTime.TotalSeconds ) );
        }
    }
}
