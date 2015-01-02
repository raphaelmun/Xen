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
    public class XenFarseer_DynamicObjectBehavior : BehaviorBase<XenFarseer_DynamicObjectBehavior>, IXenFarseerBehavior
    {
        public const float DefaultMass = 1.0f;

        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target, DefaultMass ) );
        }
        public static void AcquireAndAttach( IElement2D target, float mass )
        {
            target.Behaviors.Add( Acquire( target, mass ) );
        }
        public static XenFarseer_DynamicObjectBehavior Acquire( IElement2D target, float mass )
        {
            var instance = Acquire();
            instance.Reset( target, mass );
            return instance;
        }

        private IElement2D _target;
        private float _mass;

        public void Reset( IElement2D target, float mass )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
            _mass = mass;
            hasRunBehavior = false;
            UpdateOrder = 0;
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
                body.IsStatic = ( _mass == 0.0f );
                body.Mass = _mass;
            }
        }
    }
}
