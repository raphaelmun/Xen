using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xen2D;
using XenGameBase;
using XenFarseer;
using System.IO;

namespace Demo_FarseerPlugin_Stacked
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

    /// <summary>
    /// This demo shows how to listen on the collision event for a layer and handle collisions
    /// </summary>
    public class GameMain : GameBase
    {
        private const int numRows = 5, numCols = 10;

        protected override void Initialize()
        {
            PluginDirectory = Directory.GetCurrentDirectory();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            float vertSize = ( 480.0f ) / ( numRows + 1 );
            float horizSize = ( 800.0f ) / ( numCols + 1 );
            float scaleFactor = Math.Min( vertSize / 100.0f, horizSize / 100.0f );

            for( int y = 0; y < numRows; y++ )
            {
                for( int x = 0; x < numCols; x++ )
                {
                    MainScene.Children.Add( SquareElement.Acquire( new Vector2( horizSize * x + 64.0f, vertSize * y + 64.0f ) ) );
                }
            }

            //IElement2D controller = SquareElement.Acquire( new Vector2( 50, 50 ) );
            //controller.RenderingExtent.Scale = Vector2.One * 0.5f;
            //XenFarseer_KeyboardControlBehavior.AcquireAndAttach( controller );
            //MainScene.Children.Add( controller );

            base.LoadContent();
        }
    }
}
