using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_ModalPauseLayer
{
    public class BackgroundElement : Element2D<BackgroundElement>
    {
        private StaticSprite _backTexture = StaticSprite.Acquire( Textures.Get( TexId.GrayBackground ) );

        public override void Reset()
        {
            base.Reset();
            _backTexture.Opacity = 0.5f;
        }

        protected override void ResetDirectState()
        {
            VisualComponent = _backTexture;
            base.ResetDirectState();
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            _backTexture.Draw( spriteBatch, transformFromWorldToCamera );
        }

        public override IRectangularExtent RenderingExtent
        {
            get{ return _backTexture.RenderingExtent; }
        }
    }
}
