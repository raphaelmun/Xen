using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;
using System;

namespace Demo_MouseSelection
{
    /// <summary>
    /// Behavior that causes the target opacity to vary periodically with time.
    /// </summary>
    class FlashingBehavior : BehaviorBase<FlashingBehavior>
    {
        private IRenderable2D _target;
        private float _period;

        public void Reset( IRenderable2D target, float period )
        {
            _target = target;
            _period = period;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = true;
            _target = null;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            _target.Opacity = Math.Abs( (float)Math.Sin( gameTime.TotalGameTime.TotalSeconds ) );
        }
    }
}
