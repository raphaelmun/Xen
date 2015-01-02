using XenGameBase;
using Microsoft.Xna.Framework;
using Xen2D;

namespace TwinStickShooter
{
    public class ImpactShockwave : Element2D<ImpactShockwave>
    {
        public static ImpactShockwave Acquire( Vector2 position )
        {
            var instance = Pool.Acquire();
            instance.Reset();
            instance.RenderingExtent.Anchor = position;
            return instance;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponent = StaticSprite.Acquire( Textures.Get( TexId.Impact_Shockwave ) );
            RenderingExtent.ReAnchor( new Vector2( 100, 100 ) );

            RenderingExtent.Scale = 0.5f * Vector2.One;

            Behaviors.Add( FadeoutBehavior.Acquire( this ) );
            Behaviors.Add( ReleasesAfterTimeBehavior.Acquire( this, 1.5f, Globals.TotalGameTimeSeconds ) );
        }
    }
}
