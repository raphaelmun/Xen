using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XenGameBase;

namespace TwinStickShooter
{
    class StarfighterCommandBehavior : BehaviorBase<StarfighterCommandBehavior>
    {
        enum WingState
        {
            Folded,
            Folding,
            Expanded,
            Expanding,
        }

        public static StarfighterCommandBehavior Acquire( StarFighter target )
        {
            var instance = Pool.Acquire();
            instance.Reset( target );
            return instance;
        }

        private StarFighter _target;
        float _wingAngle = _wingFoldedAngle;
        WingState _wingState;

        const float _wingFoldedAngle = 0;
        const float _wingExpandedAngle = MathHelper.Pi / 12;
        float _wingRotationSpeedRadiansPerSecond = _wingExpandedAngle;

        public void Reset( StarFighter target )
        {
            _target = target;
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
                if( input.IsKeyPressed( Keys.Tab ) )
                {
                    switch( _wingState )
                    {
                        case WingState.Expanded:
                        case WingState.Expanding:
                            _wingState = WingState.Folding;
                            break;
                        case WingState.Folded:
                        case WingState.Folding:
                            _wingState = WingState.Expanding;
                            break;
                    }
                }
            }
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );

            float wingAngleChange = 0;
            switch( _wingState )
            {
                case WingState.Expanding:
                    wingAngleChange = _wingRotationSpeedRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case WingState.Folding:
                    wingAngleChange = -_wingRotationSpeedRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }

            if( 0 != wingAngleChange )
            {
                _wingAngle += wingAngleChange;
                if( _wingAngle >= _wingExpandedAngle )
                {
                    _wingAngle = _wingExpandedAngle;
                    _wingState = WingState.Expanded;
                }

                if( _wingAngle <= _wingFoldedAngle )
                {
                    _wingAngle = _wingFoldedAngle;
                    _wingState = WingState.Folded;
                }

                _target.SetWingAngle( _wingAngle );
            }
        }
    }
}
