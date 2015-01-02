using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    class StarFighterMoveBehavior : BehaviorBase<StarFighterMoveBehavior>
    {
        public static StarFighterMoveBehavior Acquire( IStarFighter target )
        {
            var instance = Acquire();
            instance._target = target;
            return instance;
        }

        private IStarFighter _target;

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            WantsInput = true;
            _target = null;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );

            float distancedTraveled = _target.CurrentSpeed * (float)Globals.LastUpdate.ElapsedGameTime.TotalSeconds;
            Vector2 displacement =  distancedTraveled * XenMath.CreateNormalVectorFromAngleFloat( _target.RenderingExtent.Angle - MathHelper.PiOver2 );
            _target.RenderingExtent.Anchor += displacement;
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.KeyboardProcessed )
            {
                int dAcceleration = 0;
                int dAngle = 0;
                //accelerate
                if( input.CurrentKeyboardState.IsKeyDown( Keys.W ) )
                    dAcceleration++;

                //decelerate
                if( input.CurrentKeyboardState.IsKeyDown( Keys.S ) )
                    dAcceleration--;

                //turn left
                if( input.CurrentKeyboardState.IsKeyDown( Keys.A ) )
                    dAngle--;

                //turn right
                if( input.CurrentKeyboardState.IsKeyDown( Keys.D ) )
                    dAngle++;

                _target.CurrentSpeed += dAcceleration * _target.Acceleration * Globals.LastUpdateElapsedSeconds;
                _target.CurrentSpeed = MathHelper.Clamp( _target.CurrentSpeed, 0, _target.MaxSpeed );

                _target.RenderingExtent.Angle += dAngle * _target.TurnSpeed * Globals.LastUpdateElapsedSeconds;
            }
        }
    }
}
