using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace Demo_BasicScene
{
    #region Textures
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
    #endregion

    /// <summary>
    /// This demo shows how to create a simple Element2D that draws a sprite.
    /// Move the sprite around with W,A,S,D
    /// </summary>
    public class GameMain : GameBase
    {
        Layer _layerFront;
        Layer _layerBack;
        Layer _compositeLayer;
        BasicElement _element;

        protected override void Initialize()
        {
            base.Initialize();
            _layerFront = Layer.Acquire();
            _layerBack = Layer.Acquire();
            var e1 = BasicElement.Acquire();
            e1.RenderingExtent.Anchor = Vector2.Zero;

            var e2 = BasicElement.Acquire();
            e2.RenderingExtent.Anchor = new Vector2( 300, 300 );
            _layerFront.Children.Add( e1 );
            _layerFront.Children.Add( e2 );

            var b = OutlineBehavior.Acquire();
            b.Reset( e2, Color.OrangeRed );
            e2.Behaviors.Add( b );

            _element = e2;

            var e3 = RedRectElement.Acquire();
            e3.RenderingExtent.Anchor = new Vector2( 100, 100 );
            _layerBack.Children.Add( e3 );
            var e4 = RedRectElement.Acquire();
            e4.RenderingExtent.Anchor = new Vector2( 400, 200 );
            _layerBack.Children.Add( e4 );

            _layerFront.DrawOrder = 1;
            _layerBack.DrawOrder = 0;

            _compositeLayer = Layer.Acquire();
            _compositeLayer.Children.Add( _layerBack );
            _compositeLayer.Children.Add( _layerFront );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            _layerFront.Update( gameTime );
            _layerBack.Update( gameTime );
            var ks = Keyboard.GetState();

            if( ks.IsKeyDown( Keys.Space ) )
            {
                if( _element != null )
                {
                    _element.Release();
                    _element = null;
                }
            }
            if( ks.IsKeyDown( Keys.D ) )
            {
                _layerFront.Viewport.Anchor += new Vector2( 1, 0 );
            }
            if( ks.IsKeyDown( Keys.A ) )
            {
                _layerFront.Viewport.Anchor += new Vector2( -1, 0 );
            }
            if( ks.IsKeyDown( Keys.W ) )
            {
                _layerFront.Viewport.Anchor += new Vector2( 0, -1 );
            }
            if( ks.IsKeyDown( Keys.S ) )
            {
                _layerFront.Viewport.Anchor += new Vector2( 0, 1 );
            }
            if( ks.IsKeyDown( Keys.Q ) )
            {
                _layerFront.Viewport.Angle -= 0.05f;
            }
            if( ks.IsKeyDown( Keys.E ) )
            {
                _layerFront.Viewport.Angle += 0.05f;
            }
            if( ks.IsKeyDown( Keys.Z ) )
            {
                _layerFront.Viewport.Scale *= 1.05f;
            }
            if( ks.IsKeyDown( Keys.C ) )
            {
                _layerFront.Viewport.Scale /= 1.05f;
            }
        }

        protected override void DrawInternal( GameTime gameTime )
        {
            _compositeLayer.Draw( SpriteRenderer );
        }
    }
}
