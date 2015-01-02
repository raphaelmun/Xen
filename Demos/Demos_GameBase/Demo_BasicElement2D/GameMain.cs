using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace Demo_BasicElement2D
{
    /// <summary>
    /// This demo shows how to create a simple Element2D that draws a sprite.
    /// Move the sprite around with W,A,S,D
    /// </summary>
    public enum TexId : int
    {
        [ContentIdentifier( "textures\\rect_blue_horizontal_200x100" )]
        RectBlueHorizontal,
        [ContentIdentifier( "textures\\rect_red_vertical_100x200" )]
        RectRedVertical,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    public class Textures : Texture2DCache<TexId>
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

    public class GameMain : GameBase
    {
        private BasicElement _basicElement;
        private IBehavior _behavior;

        protected override void Initialize()
        {
            base.Initialize();
            _basicElement = BasicElement.Acquire();

            var behavior = MoveToBehavior.Acquire();
            //Move _basicElement to (500,500) at 10 units per second.  
            behavior.Reset( new Vector2( 500, 500 ), 10, _basicElement );
            _basicElement.Behaviors.Add( behavior );
            _behavior = behavior;

            var behavior2 = OutlineBehavior.Acquire();
            //Add an outline behavior, which draws the rendering extent outline
            behavior2.Reset( _basicElement, Color.OrangeRed );
            _basicElement.Behaviors.Add( behavior2 );
        }

        protected override void DrawInternal( GameTime gameTime )
        {
            _basicElement.Draw( SpriteRenderer );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            _basicElement.Update( gameTime );

            if( Keyboard.GetState().IsKeyDown( Keys.Space ) )
            {
                if( _behavior != null )
                {
                    _behavior.Release();
                    _behavior = null;
                }
            }
        }
    }
}
