using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Xen2D;

namespace XenFarseer
{
    public class XenFarseer_ChaseCursorBehavior : BehaviorBase<XenFarseer_ChaseCursorBehavior>, IXenFarseerBehavior
    {
        public const float DefaultMoveSpeed = 500.0f;//200.0f;

        public static void AcquireAndAttach( IElement2D target )
        {
            target.Behaviors.Add( Acquire( target, DefaultMoveSpeed ) );
        }
        public static void AcquireAndAttach( IElement2D target, float moveSpeed )
        {
            target.Behaviors.Add( Acquire( target, moveSpeed ) );
        }
        public static XenFarseer_ChaseCursorBehavior Acquire( IElement2D target, float moveSpeed )
        {
            var instance = Acquire();
            instance.Reset( target, moveSpeed );
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
                body.IgnoreGravity = true;
                body.IsStatic = false;
                body.Mass = 5.0f;
            }
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.KeyboardProcessed && _body != null )
            {
                Vector2 direction = input.CurrentMousePosition - _body.Position;
                float distance = direction.Length();
                if( distance > 50.0f )
                {
                    direction.Normalize();
                    _body.LinearVelocity = direction * _moveSpeed;
                    //_body.ApplyForce( direction * _moveSpeed );
                }
                else
                {
                    direction.Normalize();
                    _body.LinearVelocity = direction * Interpolator.CircularIn( _moveSpeed, 0.0f, 50.0f - distance, 50.0f );
                    //_body.ApplyForce( direction * _moveSpeed );
                }
            }
        }
    }
}
