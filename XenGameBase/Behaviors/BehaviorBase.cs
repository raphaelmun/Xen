using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    /// <summary>
    /// Base class for Behaviors.  A Behavior is a unit of functionality for drawing/updating/processing input on an element.  
    /// </summary>
    /// 
    /// <example>
    /// FadeOverTimeBehavior- fade an element over time (for particles)
    /// TimeToLiveBehavior- release an element after ttl has elapsed (for bullets)
    /// LinearMoveBehavior- scroll a text element on the screen
    /// </example>
    /// <typeparam name="T">The derived type.</typeparam>
    public abstract class BehaviorBase<T> : Component2D<T>, IBehavior
        where T : ComposableObject, new()
    {
        private IElement2D _parent;

        /// <summary>
        /// Gets or sets the draw order.  Behavior draw orders are restricted to the order they are updated.
        /// For behaviors, this property is a pass-through to UpdateOrder.
        /// </summary>
        public override int DrawOrder 
        {
            get { return UpdateOrder; }
            set { UpdateOrder = value; } 
        }

        public IElement2D Parent
        {
            get { return _parent; }
            set
            {
                if( ( null != _parent ) && ( null != value ) )
                    throw new InvalidOperationException( "cannot set parent twice.  This object must be released and reacquired." );
                _parent = value;
            }
        }

        public bool WantsInput { get; protected set; }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera ) { }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            WantsInput = false;
        }

        public void ProcessInput( ref InputState input, Matrix transformFromCameraToWorld ) 
        {
            if( WantsInput )
                ProcessInputInternal( ref input, transformFromCameraToWorld );
        }

        protected virtual void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld ) { }
    }

    /// <summary>
    /// Base class for a behavior that acts on targets of the specific generic type.
    /// </summary>
    /// <typeparam name="T">The derived type.</typeparam>
    /// <typeparam name="TargetType">The target type that this behavior operates on.</typeparam>
    public class TargettedBehavior<T, TargetType> : BehaviorBase<TargettedBehavior<T, TargetType>>, IBehavior
        where T : ComposableObject, new()
    {
        protected TargetType _target;

        public static TargettedBehavior<T, TargetType> Acquire( TargetType target )
        {
            var instance = Acquire();
            instance._target = target;
            return instance;
        }

        //protected override void ResetDirectState()
        //{
        //    base.ResetDirectState();
        //    _target = default( TargetType );
        //}
    }
}
