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
    public class XenFarseer_StaticObjectBehavior : BehaviorBase<XenFarseer_StaticObjectBehavior>, IXenFarseerBehavior
    {
        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target ) );
        }
        public static XenFarseer_StaticObjectBehavior Acquire( IElement2D target )
        {
            var instance = Acquire();
            instance.Reset( target );
            return instance;
        }

        private IElement2D _target;

        public void Reset( IElement2D target )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
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
                body.IsStatic = true;
            }
        }
    }
}
