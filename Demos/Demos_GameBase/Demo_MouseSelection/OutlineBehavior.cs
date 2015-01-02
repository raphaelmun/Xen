using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_MouseSelection
{
    /// <summary>
    /// Simple example behavior that outlines (draws) the current extent if the mouse is within it
    /// </summary>
    class OutlineBehavior : BehaviorBase<OutlineBehavior>
    {
        private IRenderable2D _target;
        private Color _outlineColor;

        public void Reset( IRenderable2D target, Color outlineColor )
        {
            _target = target;
            _outlineColor = outlineColor;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _target = null;
            WantsInput = true;
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawExtent( _target.RenderingExtent, _outlineColor, transformFromWorldToCamera );
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( (!input.MouseProcessed) && input.LeftButtonPressed )
            {
                Vector2 mousePosInWorld = Vector2.Transform( input.CurrentMousePosition, transformFromCameraToWorld );
                if( _target.RenderingExtent.ContainsPoint( mousePosInWorld ) )
                {
                    Visible = !Visible;
                    input.MouseProcessed = true;
                }
            }
        }
    }
}
