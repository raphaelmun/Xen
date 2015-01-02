using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Xen2D;

namespace PlatformerXen
{
    public enum SongId : int
    {
        [ContentIdentifier( "Sounds\\music" )]
        Music,
    }

    class Songs : SongCache<SongId>
    {
        public static readonly Songs Instance = new Songs();

        public static Song Get( SongId song )
        {
            return Instance[ song ];
        }

        public override Song this[ SongId song ]
        {
            get { return this[ (int)song ]; }
        }
    }

    public enum SoundId : int
    {
        [ContentIdentifier( "Sounds\\playerjump" )]
        PlayerJump,
        [ContentIdentifier( "Sounds\\GemCollected" )]
        GemCollected,
    }

    class SoundEffects : SoundEffectCache<SoundId>
    {
        public static readonly SoundEffects Instance = new SoundEffects();

        public static SoundEffect Get( SoundId song )
        {
            return Instance[ song ];
        }

        public override SoundEffect this[ SoundId song ]
        {
            get { return this[ (int)song ]; }
        }
    }
}
