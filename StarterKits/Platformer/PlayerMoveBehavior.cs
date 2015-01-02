using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace PlatformerXen
{
    class PlayerMoveBehavior : BehaviorBase<PlayerMoveBehavior>
    {
        public static PlayerMoveBehavior Acquire( Player player, float moveSpeed )
        {
            var instance = _pool.Acquire();
            instance.Reset( player, moveSpeed );
            return instance;
        }

        private Player _player;
        private float _moveSpeed;

        public void Reset( Player player, float moveSpeed )
        {
            _player = player;
            _moveSpeed = moveSpeed;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _player = null;
            WantsInput = true;
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.KeyboardProcessed )
            {
                if( input.CurrentKeyboardState.IsKeyDown( Keys.A ) )
                {
                    _player.Velocity = new Vector2( -_moveSpeed, _player.Velocity.Y );
                }
                else if( input.CurrentKeyboardState.IsKeyDown( Keys.D ) )
                {
                    _player.Velocity = new Vector2( _moveSpeed, _player.Velocity.Y );
                }

                if( input.CurrentKeyboardState.IsKeyUp( Keys.A ) && input.CurrentKeyboardState.IsKeyUp( Keys.D ) )
                    _player.Velocity = new Vector2( 0, _player.Velocity.Y );

                if( input.IsKeyPressed( Keys.Space ) )
                {
                    if( _player.SupportingPlatforms.Count > 0 )
                    {
                        _player.Velocity = new Vector2( _player.Velocity.X, -100 );
                        SoundEffects.Get( SoundId.PlayerJump ).Play();
                    }
                }

                if( input.IsKeyPressed( Keys.Tab ) )
                {
                    _player.RenderingExtent.Anchor = new Vector2( 50, 50 );
                    _player.Velocity = Vector2.Zero;
                }
            }
        }
    }
}
