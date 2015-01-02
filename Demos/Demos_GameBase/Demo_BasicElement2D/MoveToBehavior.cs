using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace Demo_BasicElement2D
{
    /// <summary>
    /// Simple example behavior that shows how to move an element to a specified destination vector
    /// </summary>
    public class MoveToBehavior : BehaviorBase<MoveToBehavior>
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
            Vector2 vectorToDisplace = deltaFromTarget * _velocity * secondsElapsed;

            _target.RenderingExtent.Anchor += vectorToDisplace;
        }
    }
}
