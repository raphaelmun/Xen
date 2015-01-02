using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace PlatformerXen
{
    public class Gem : Element2D<Gem>
    {
        StaticSprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.Gem ) );

        public override void Reset()
        {
            VisualComponent = _sprite;
            base.Reset();
            ModulationColor = Color.Yellow;
            CollisionClass = (int)PlatformerCollisionClass.Gem;
        }
    }
}
