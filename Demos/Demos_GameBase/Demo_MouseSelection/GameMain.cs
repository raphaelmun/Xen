using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_MouseSelection
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\cursor" )]
        Cursor,
        [ContentIdentifier( "textures\\simple_square_64x64" )]
        Square,
        [ContentIdentifier( "textures\\gray_rect_100x200" )]
        GrayRect,
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
    /// This demo contains two layers.  The top layer contains a set of composite elements, squares joined at a single corner.  
    /// Clicking on an item selects it.  Notice that the click is received by the "topmost" element on the draw z-order.  
    /// The bottom layer contains a rectangle that can be selected in similar fashion.  
    /// </summary>
    public class GameMain : GameBase
    {
        Layer _topLayer;
        Layer _bottomLayer;

        protected override void LoadContent()
        {
            BackgroundColor = Color.WhiteSmoke;
            _topLayer = Layer.Acquire( 1 ); //Draw order 1 so that it is on top

            for( int i = 0; i < 3; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    var element = ClickableElement.Acquire();
                    element.RenderingExtent.Anchor += new Vector2( 50 + i * 100, 50 + j * 100 );
                    element.DrawOrder = i + j;
                    _topLayer.Children.Add( element );
                }
            }
            
            _bottomLayer = Layer.Acquire( 0 );  //Draw order 0 so that this layer is below the top layer.
            _bottomLayer.Opacity = 0.5f;
            MouseScrollZoomBehavior.AcquireAndAttach( _bottomLayer );

            var elt = ClickableElement2.Acquire();
            elt.RenderingExtent.Anchor += new Vector2( 200, 200 );
            _bottomLayer.Children.Add( elt );

            MainScene.Children.Add( _topLayer );
            MainScene.Children.Add( _bottomLayer );
            MouseScrollZoomBehavior.AcquireAndAttach( MainScene );
        }
    }
}
