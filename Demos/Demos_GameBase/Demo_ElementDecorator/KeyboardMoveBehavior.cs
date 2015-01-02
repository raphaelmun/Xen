using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace Demo_ElementDecorator
{
    public class KeyboardMoveBehavior : BehaviorBase<KeyboardMoveBehavior>
    {
        public static KeyboardMoveBehavior Acquire( IRenderable2D target, float moveSpeed )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, moveSpeed );
            return instance;
        }

        private IRenderable2D _target;
        private float _moveSpeed;

        public void Reset( IRenderable2D target, float moveSpeed )
        {
            _target = target;
            _moveSpeed = moveSpeed;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _target = null;
            WantsInput = true;
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.KeyboardProcessed )
            {
                float dX = 0;
                float dY = 0;
                float dAngle = 0;
                float dZoom = 1;

                if( input.CurrentKeyboardState.IsKeyDown( Keys.W ) )
                    dY -= _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.S ) )
                    dY += _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.A ) )
                    dX -= _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.D ) )
                    dX += _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.Q ) )
                    dAngle -= 0.02f;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.E ) )
                    dAngle += 0.02f;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.Z ) )
                    dZoom *= 1.02f;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.C ) )
                    dZoom /= 1.02f;

                _target.RenderingExtent.Anchor += new Vector2( dX, dY );
                _target.RenderingExtent.Angle += dAngle;
                _target.RenderingExtent.Scale *= dZoom;
            }
        }
    }
}
