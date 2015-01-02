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
    public class XenFarseer_InitialMovementBehavior : BehaviorBase<XenFarseer_InitialMovementBehavior>, IXenFarseerBehavior
    {
        public static Vector2 DefaultMovement = Vector2.One;

        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target, DefaultMovement ) );
        }
        public static void AcquireAndAttach( IElement2D target, Vector2 velocity )
        {
            target.Behaviors.Add( Acquire( target, velocity ) );
        }
        public static XenFarseer_InitialMovementBehavior Acquire( IElement2D target, Vector2 velocity )
        {
            var instance = Acquire();
            instance.Reset( target, velocity );
            return instance;
        }

        private IElement2D _target;
        private Vector2 _velocity;

        public void Reset( IElement2D target, Vector2 velocity )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
            _velocity = velocity;
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
                body.LinearVelocity = _velocity;
            }
        }
    }
}
