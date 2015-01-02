using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace XenFarseer
{
    public class XenFarseer_KeyboardControlBehavior : BehaviorBase<XenFarseer_KeyboardControlBehavior>, IXenFarseerBehavior
    {
        public const float DefaultMoveForce = 10000.0f;

        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target, DefaultMoveForce ) );
        }
        public static void AcquireAndAttach( IElement2D target, float moveForce )
        {
            target.Behaviors.Add( Acquire( target, moveForce ) );
        }
        public static XenFarseer_KeyboardControlBehavior Acquire( IElement2D target, float moveForce )
        {
            var instance = Acquire();
            instance.Reset( target, moveForce );
            return instance;
        }

        private IElement2D _target;
        private float _moveSpeed;

        public void Reset( IElement2D target, float moveSpeed )
        {
            Debug.Assert( null != target, "target cannot be null" );
            _target = target;
            _moveSpeed = moveSpeed;
            hasRunBehavior = false;
            UpdateOrder = 100;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _target = null;
            WantsInput = true;
        }

        Body _body = null;
        bool hasRunBehavior;
        //public bool IsOneTimeBehavior { get { return false; } }

        public void RunBehavior( Body body, GameTime gameTime )
        {
            if( !hasRunBehavior )
            {
                hasRunBehavior = true;
                _body = body;
                body.FixedRotation = true;
                body.SleepingAllowed = false;
                body.IsStatic = false;
                body.Mass = 5.0f;
            }
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.KeyboardProcessed && _body != null )
            {
                float dX = 0;
                float dY = 0;

                if( input.CurrentKeyboardState.IsKeyDown( Keys.W ) )
                    dY -= _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.S ) )
                    dY += _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.A ) )
                    dX -= _moveSpeed;
                if( input.CurrentKeyboardState.IsKeyDown( Keys.D ) )
                    dX += _moveSpeed;

                //_body.LinearVelocity = new Vector2( dX, dY );
                _body.ApplyForce( new Vector2( dX, dY ) );
            }
        }
    }
}
