using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    public enum CustomCollisionClass : uint
    { 
        Player = 1,
        Projectile = 2,
    }

    public sealed class CollisionRules : CollisionRuleSet
    {
        public CollisionRules()
        {
            AddRule( (uint)CustomCollisionClass.Player, (uint)CustomCollisionClass.Projectile, true, 0.1f );
        }
    }

    public partial class Game : GameBase
    {
        protected override void OnMainSceneCollisionHandler( CollisionEventArgs collisionEvent )
        {

            if( collisionEvent.InvolvesTypes<Laser, StarFighter>() )
            {
                var laser = collisionEvent.GetParticipantOfType<Laser>();
                var fighter = collisionEvent.GetParticipantOfType<StarFighter>();

                fighter.ApplyHitFrom( laser );

                laser.Release();
                MainScene.Children.Add( ImpactShockwave.Acquire( laser.RenderingExtent.ActualCenter ) );
                MainScene.Behaviors.Add( ShakeViewportBehavior.Acquire( MainScene ) );
            }
        }
    }
}
