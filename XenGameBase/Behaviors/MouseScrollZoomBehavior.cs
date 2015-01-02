using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace XenGameBase
{
    /// <summary>
    /// This behavior zooms the camera of the attached layer.
    /// </summary>
    public class MouseScrollZoomBehavior : BehaviorBase<MouseScrollZoomBehavior>
    {
        public const float DefaultZoomFactorPerScrollUnit = 1.02f;

        public static void AcquireAndAttach( ILayer target )
        {
            target.Behaviors.Add( Acquire( target, DefaultZoomFactorPerScrollUnit ) );
        }

        public static void AcquireAndAttach( ILayer target, float zoomFactorPerScrollUnit )
        {
            target.Behaviors.Add( Acquire( target, zoomFactorPerScrollUnit ) );
        }

        public static MouseScrollZoomBehavior Acquire( ILayer target, float zoomFactorPerScrollUnit )
        {
            var instance = Acquire();
            instance.Reset( target, zoomFactorPerScrollUnit );
            return instance;
        }

        private ILayer _target;
        private float _zoomFactorPerScrollUnit;

        public void Reset( ILayer target, float zoomFactorPerScrollUnit )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
            _zoomFactorPerScrollUnit = zoomFactorPerScrollUnit;
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

            if( input.ScrollWheelValueDelta != 0 )
            {
                float zoom = (float)Math.Pow( _zoomFactorPerScrollUnit, -input.ScrollWheelValueDelta / 100 );
                _target.Viewport.Scale *= zoom;
            }
        }
    }
}
