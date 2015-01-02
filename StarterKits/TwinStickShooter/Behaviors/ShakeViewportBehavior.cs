using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    /// <summary>
    /// Behavior that periodically applies a displacement to the specified layer to "shake" it.  
    /// </summary>
    class ShakeViewportBehavior : BehaviorBase<ShakeViewportBehavior>
    {
        public static ShakeViewportBehavior Acquire( ILayer target )
        {
            var instance = _pool.Acquire();
            instance.Reset( target );
            return instance;
        }

        private ILayer _target;
        private float _shakeMagnitude = 20;
        private float _duration = .5f;
        private float _shakePeriod = 0.05f;
        private float _elapsedTimeSinceLastShake;
        private int _lastShakeDirection = 1;

        public void Reset( ILayer target )
        {
            _target = target;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _target = null;
            _duration = .5f;
            _elapsedTimeSinceLastShake = 0;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            _elapsedTimeSinceLastShake += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if( _duration <= 0 )
            {
                Release();
            }

            if( _elapsedTimeSinceLastShake > _shakePeriod )
            {
                _lastShakeDirection *= -1;
                _elapsedTimeSinceLastShake = 0;
                _target.Viewport.Anchor += _lastShakeDirection * _shakeMagnitude * _duration * Vector2.One;
            }
        }
    }
}
