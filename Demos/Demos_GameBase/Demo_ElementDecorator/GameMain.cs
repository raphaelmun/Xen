using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_ElementDecorator
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\colored_rect_200x200" )]
        ColoredRect,
        [ContentIdentifier( "textures\\marker_red" )]
        Marker_Red,
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
    #endregion

    #region Font Cache

    enum FontId : int
    {
        [ContentIdentifier( "Arial" )]
        Arial
    }

    class Fonts : SpriteFontCache<FontId>
    {
        public static readonly Fonts Instance = new Fonts();

        public static SpriteFont Get( FontId font )
        {
            return Instance[ font ];
        }

        public override SpriteFont this[ FontId font ]
        {
            get { return this[ (int)font ]; }
        }
    }
    #endregion

    /// <summary>
    /// This demo shows how element decorators work.  The SquareElement class is decorated with markers and text denoting each corner.
    /// W,A,S,D move the square.  
    /// Q,E rotate the square.
    /// Z,C zoom in and out.  
    /// 
    /// No matter the transform of the square, the decorators attach to an anchor point but are otherwise unaffected by the 
    /// element's transforms.  This is very useful for displaying things like player names, health bars, etc. that would not be 
    /// appropriate to attach to a composite element's children.  
    /// </summary>
    public class GameMain : GameBase
    {
        protected override void LoadContent()
        {
            base.LoadContent();
            MainScene.Children.Add( SquareElement.Acquire( new Vector2( 200, 200 ) ) );
        }
    }
}
