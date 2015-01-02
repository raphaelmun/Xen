using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace XenFarseer
{
    public class XenFarseer_RotationBehavior : BehaviorBase<XenFarseer_RotationBehavior>, IXenFarseerBehavior
    {
        public const float DefaultAngularVelocity = 1.0f;

        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target, DefaultAngularVelocity ) );
        }
        public static void AcquireAndAttach( IElement2D target, float angularVelocity )
        {
            target.Behaviors.Add( Acquire( target, angularVelocity ) );
        }
        public static XenFarseer_RotationBehavior Acquire( IElement2D target, float angularVelocity )
        {
            var instance = Acquire();
            instance.Reset( target, angularVelocity );
            return instance;
        }

        private IElement2D _target;
        private float _angularVelocity;

        public void Reset( IElement2D target, float angularVelocity )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
            _angularVelocity = angularVelocity;
            hasRunBehavior = false;
            UpdateOrder = 100;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _target = null;
            WantsInput = false;
        }

        bool hasRunBehavior;
        //public bool IsOneTimeBehavior { get { return true; } }

        public void RunBehavior( Body body, GameTime gameTime )
        {
            if( !hasRunBehavior )
            {
                hasRunBehavior = true;
                body.AngularVelocity = _angularVelocity;
            }
        }
    }
}
