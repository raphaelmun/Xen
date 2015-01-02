using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_MouseSelection
{
    /// <summary>
    /// Behavior that rotates the target extent every update.
    /// </summary>
    class RotationBehavior : BehaviorBase<RotationBehavior>
    {
        private IRenderable2D _target;
        private float _rotationPerUpdate;

        public void Reset( IRenderable2D target, float rotationPerUpdate )
        {
            _target = target;
            _rotationPerUpdate = rotationPerUpdate;
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
            _target.RenderingExtent.Angle += _rotationPerUpdate;
        }
    }
}
