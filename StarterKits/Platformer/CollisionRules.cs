using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace PlatformerXen
{
    public enum PlatformerCollisionClass : uint
    {
        Player = 1,
        Enemy = 2,
        Gem = 3,
        Tile_Platform = 4,
    }

    public class PlatformerCollisionRules : CollisionRuleSet
    {
        public PlatformerCollisionRules()
        {
            AddRule( (uint)PlatformerCollisionClass.Player, (uint)PlatformerCollisionClass.Tile_Platform, true, 0 );
            AddRule( (uint)PlatformerCollisionClass.Player, (uint)PlatformerCollisionClass.Gem, true, 0 );
        }
    }
}
