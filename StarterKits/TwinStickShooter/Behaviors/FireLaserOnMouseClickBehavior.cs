using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;

namespace TwinStickShooter
{
    class FireLaserOnMouseClickBehavior : BehaviorBase<FireLaserOnMouseClickBehavior>
    {
        private Layer _mainScene;
        private float _moveSpeed;
        private ISprite _sprite = StaticSprite.Acquire( Textures.Get( TexId.Marker_Red ) );
        Vector2 _laserOrigin = new Vector2( 700, 300 );

        public void Reset( Layer mainScene, float moveSpeed )
        {
            _mainScene = mainScene;
            _moveSpeed = moveSpeed;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = true;
            _mainScene = null;
            WantsInput = true;
            _sprite.RenderingExtent.Anchor = _laserOrigin;
        }

        protected override void DrawInternal( Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawSprite( _sprite );
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.MouseProcessed )
            {
                if( ( input.CurrentMouseState.RightButton == ButtonState.Pressed ) &&
                    ( input.LastMouseState.RightButton == ButtonState.Released ) )
                {
                    _laserOrigin = input.CurrentMousePosition;
                    _sprite.RenderingExtent.Anchor = _laserOrigin;
                }

                if( ( input.CurrentMouseState.LeftButton == ButtonState.Pressed ) &&
                    ( input.LastMouseState.LeftButton == ButtonState.Released ) )
                {
                    Vector2 originToClick = input.CurrentMousePosition - _laserOrigin;
                    float angle = XenMath.GetAngleFloat( originToClick );

                    var laser = Laser.Acquire();
                    laser.RenderingExtent.Anchor = _laserOrigin;
                    laser.RenderingExtent.Angle = angle;
                    _mainScene.Children.Add( laser );
                }
            }
        }
    }
}
