using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    /// <summary>
    /// Behavior that moves the target along the direction of its current angle.
    /// </summary>
    public class MoveForwardBehavior : BehaviorBase<MoveForwardBehavior>
    {
        public static MoveForwardBehavior Acquire( IElement2D target, float speed )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, speed );
            return instance;
        }

        IElement2D _target;
        float _speed;

        public void Reset( IElement2D target, float speed )
        {
            _target = target;
            _speed = speed;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _target = null;
            _speed = 0;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            Vector2 displacement = _speed * XenMath.Rotate( Vector2.UnitX, _target.RenderingExtent.Angle );
            _target.RenderingExtent.Anchor += displacement;
        }
    }
}
