using XenGameBase;
using Microsoft.Xna.Framework;
using Xen2D;

namespace TwinStickShooter
{
    public class ReleasesAfterTimeBehavior : BehaviorBase<ReleasesAfterTimeBehavior>
    {
        public static ReleasesAfterTimeBehavior Acquire( IElement2D target, float timeToLive )
        {
            return Acquire( target, timeToLive, Globals.TotalGameTimeSeconds );
        }

        public static ReleasesAfterTimeBehavior Acquire( IElement2D target, float timeToLive, float startTime )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, timeToLive, startTime );
            return instance;
        }

        IElement2D _target;
        float _timeToLive;
        float _startTime;

        public void Reset( IElement2D target, float timeToLive, float startTime )
        {
            _target = target;
            _timeToLive = timeToLive;
            _startTime = startTime;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _target = null;
            _timeToLive = 0;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            if( ( (float)gameTime.TotalGameTime.TotalSeconds - _startTime ) > _timeToLive )
            {
                _target.Release();
            }
        }
    }
}
