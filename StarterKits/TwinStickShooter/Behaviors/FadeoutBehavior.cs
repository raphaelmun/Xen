using XenGameBase;
using Microsoft.Xna.Framework;
using Xen2D;

namespace TwinStickShooter
{
    public class FadeoutBehavior : BehaviorBase<FadeoutBehavior>
    {
        public static FadeoutBehavior Acquire( IElement2D target )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, Globals.TotalGameTimeSeconds );
            return instance;
        }

        IElement2D _target;
        float _startTime;
        const float _expansionInterval = 0.05f;

        public void Reset( IElement2D target, float startTime )
        {
            _target = target;
            _startTime = startTime;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            DrawOrder = -15;
            _target = null;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            float opacityChangePerSecond = -3f; //fades to 0 after 1 second

            float totalGametime = (float)gameTime.TotalGameTime.TotalSeconds;
            if( totalGametime - _startTime > _expansionInterval )
            {
                //Fade out because it is past expansion interval
                _target.Opacity += ( opacityChangePerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds );
            }
            else
            {
                _target.RenderingExtent.Scale += (float)( 0.5f / 0.2f * gameTime.ElapsedGameTime.TotalSeconds ) * Vector2.One;
            }
        }
    }
}
