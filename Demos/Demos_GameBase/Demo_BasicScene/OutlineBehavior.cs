using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_BasicScene
{
    /// <summary>
    /// Simple example behavior that outlines (draws) the current extent if the mouse is within it
    /// </summary>
    public class OutlineBehavior : BehaviorBase<OutlineBehavior>
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
            Visible = true;
            _target = null;
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            spriteBatch.DrawPolygon( _target.RenderingExtent, _outlineColor, transformFromWorldToCamera );
        }
    }
}
