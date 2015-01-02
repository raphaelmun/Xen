using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    /// <summary>
    /// Behavior that moves in a specific vector at a constant velocity.
    /// </summary>
    public class LinearMoveBehavior : BehaviorBase<LinearMoveBehavior>
    {
        public static LinearMoveBehavior Acquire( IElement2D target, Vector2 velocity )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, velocity );
            return instance;
        }

        IElement2D _target;
        Vector2 _velocity;

        public void Reset( IElement2D target, Vector2 velocity )
        {
            _target = target;
            _velocity = velocity;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _target = null;
            _velocity = Vector2.Zero;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            Vector2 displacement = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _target.RenderingExtent.Anchor += displacement;
        }
    }
}
