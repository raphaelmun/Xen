using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_ModalPauseLayer
{
    public class MoveRightBehavior : BehaviorBase<MoveRightBehavior>
    {
        private IRenderable2D _target;
        private Vector2 _velocity;

        public void Reset( IRenderable2D target, Vector2 velocity )
        {
            _target = target;
            _velocity = velocity;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            Vector2 displacement = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _target.RenderingExtent.Anchor += displacement;
        }
    }
}
