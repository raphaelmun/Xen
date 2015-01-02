using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    public class ExplodesAfterTimeBehavior : BehaviorBase<ExplodesAfterTimeBehavior>
    {
        public static ExplodesAfterTimeBehavior Acquire( IElement2D target, ILayer layer, float timeToLive )
        {
            return Acquire( target, layer, timeToLive, Globals.TotalGameTimeSeconds );
        }

        public static ExplodesAfterTimeBehavior Acquire( IElement2D target, ILayer layer, float timeToLive, float startTime )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, layer, timeToLive, startTime );
            return instance;
        }

        IElement2D _target;
        ILayer _layer;
        float _timeToLive;
        float _startTime;

        public void Reset( IElement2D target, ILayer layer, float timeToLive, float startTime )
        {
            _target = target;
            _layer = layer;
            _timeToLive = timeToLive;
            _startTime = startTime;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _target = null;
            _layer = null;
            _timeToLive = 0;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            if( ( (float)gameTime.TotalGameTime.TotalSeconds - _startTime ) > _timeToLive )
            {
                _target.Release();
                Vector2 loc = _target.Transform.TranslateVectorFromThisSpaceToAbsolute( _target.RenderingExtent.ActualCenter );
                _layer.Children.Add( ImpactShockwave.Acquire( loc ) );
            }
        }
    }
}
