using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_ModalPauseLayer
{
    public class MovingElement : Element2D<MovingElement>
    {
        private StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.GrayRect ) );

        public override void Reset()
        {
            base.Reset();
            var behavior = MoveRightBehavior.Acquire();
            behavior.Reset( this, new Vector2( 25, 0 ) );
            Behaviors.Add( behavior );
        }

        protected override void ResetDirectState()
        {
            VisualComponent = _texture;
            base.ResetDirectState();
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            _texture.Draw( spriteBatch, transformFromWorldToCamera );
        }

        public override IRectangularExtent RenderingExtent
        {
            get{ return _texture.RenderingExtent; }
        }
    }
}
