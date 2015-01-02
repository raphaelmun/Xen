using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    /// <summary>
    /// Behavior that rotates the target extent every update.
    /// </summary>
    class RotationBehavior : BehaviorBase<RotationBehavior>
    {
        public static RotationBehavior Acquire( IRenderable2D target, float rotationSpeed )
        {
            var instance = Pool.Acquire();
            instance.Reset( target, rotationSpeed );
            return instance;
        }

        private IRenderable2D _target;
        private float _rotationSpeed;

        /// <summary>
        /// Resets the instance using the specified parameters.
        /// </summary>
        /// <param name="target">The target to apply the behavior to.</param>
        /// <param name="rotationSpeed">The rotation speed in radians per second.</param>
        public void Reset( IRenderable2D target, float rotationSpeed )
        {
            _target = target;
            _rotationSpeed = rotationSpeed;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Enabled = true;
            _target = null;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            _target.RenderingExtent.Angle += _rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
