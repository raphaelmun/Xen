using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace PlatformerXen
{
    public enum TexId : int
    {
        [ContentIdentifier( "sprites\\gem" )]
        Gem,
        [ContentIdentifier( "tiles\\blockb0" )]
        BlockB0,
        [ContentIdentifier( "sprites/player/idle" )]
        Player_Idle,
        [ContentIdentifier( "sprites/player/run" )]
        Player_Run,
        [ContentIdentifier( "sprites/player/jump" )]
        Player_Jump,
        [ContentIdentifier( "sprites/player/celebrate" )]
        Player_Celebrate,

        [ContentIdentifier( "backgrounds/Layer0_0" )]
        Layer0_0,
        [ContentIdentifier( "backgrounds/Layer1_0" )]
        Layer1_0,
        [ContentIdentifier( "backgrounds/Layer2_0" )]
        Layer2_0,
    }

    class Textures : Texture2DCache<TexId>
    {
        public static readonly Textures Instance = new Textures();

        public static Texture2D Get( TexId tex )
        {
            return Instance[ tex ];
        }

        public override Texture2D this[ TexId tex ]
        {
            get { return this[ (int)tex ]; }
        }
    }
}
