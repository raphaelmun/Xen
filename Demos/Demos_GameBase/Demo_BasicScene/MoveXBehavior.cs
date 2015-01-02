using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace Demo_BasicScene
{
    public class MoveXBehavior : BehaviorBase<MoveXBehavior>
    {
        private Vector2 _destination;
        private float _velocity;
        private IRenderable2D _target;

        public void Reset( Vector2 destination, float velocity, IRenderable2D target )
        {
            _destination = destination;
            _velocity = velocity;
            _target = target;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            Vector2 deltaFromTarget = _destination - _target.RenderingExtent.Anchor;
            deltaFromTarget.Normalize();

            float secondsElapsed = (float)gameTime.ElapsedGameTime.Milliseconds / (float)1000;
            float xDisplace = _velocity * secondsElapsed;

            _target.RenderingExtent.Anchor += new Vector2( xDisplace, 0 );
        }
    }
}
