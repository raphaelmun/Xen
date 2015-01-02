using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace TwinStickShooter
{
    enum TexId : int
    {
        [ContentIdentifier( "textures\\cockpit_red" )]
        Cockpit_Red,
        [ContentIdentifier( "textures\\engine_pod_left" )]
        Engine_Pod_Left,
        [ContentIdentifier( "textures\\engine_pod_right" )]
        Engine_Pod_Right,
        [ContentIdentifier( "textures\\fuselage" )]
        Fuselage,
        [ContentIdentifier( "textures\\wing_left" )]
        Wing_Left,
        [ContentIdentifier( "textures\\wing_right" )]
        Wing_Right,
        [ContentIdentifier( "textures\\laser" )]
        Laser,
        [ContentIdentifier( "textures\\marker_red" )]
        Marker_Red,
        [ContentIdentifier( "textures\\impact_shockwave" )]
        Impact_Shockwave,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
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
