using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    /// <summary>
    /// Interface for an element decorator, which is a component that decorates an element
    /// </summary>
    public interface IElementDecorator : IPooledObject
    {
        /// <summary>
        /// The point in the parent element's extent that this decorator attaches to.
        /// </summary>
        Vector2 AnchorToParent { get; set; }

        /// <summary>
        /// The offset to apply the anchor.  Use this property to position the decorator relative to the 
        /// anchor.
        /// </summary>
        /// 
        /// <example>
        /// Set Offset to (100, 0) to position the decorator 100 pixels to the right of the anchor.
        /// </example>
        Vector2 Offset { get; set; }

        /// <summary>
        /// The decorator component.  
        /// </summary>
        IRenderable2D Component{ get; }
    }

    public sealed class ElementDecorator : ComposableObject<ElementDecorator>, IElementDecorator
    {
        public static ElementDecorator Acquire( IRenderable2D component, Vector2 anchorToParent, Vector2 offset )
        {
            var instance = Acquire();
            instance.Reset( component, anchorToParent, offset );
            return instance;
        }

        public Vector2 AnchorToParent { get; set; }
        public Vector2 Offset { get; set; }
        public IRenderable2D Component { get; private set; }

        //Cache the delegate so that it does not need to be allocated from the heap.
        private Action<IPooledObject> _onComponentReleased;

        public ElementDecorator()
        {
            _onComponentReleased = new Action<IPooledObject>( OnComponentReleased );
        }

        private void OnComponentReleased( IPooledObject component )
        {
            Component = null;
            if( IsAcquired )
                Release();
        }

        public void Reset( IRenderable2D component )
        {
            Reset( component, Vector2.Zero, Vector2.Zero );
        }

        public void Reset( IRenderable2D component, Vector2 anchorToParent, Vector2 offset )
        {
            Reset();
            Debug.Assert( null != component );
            Component = component;
            Component.OnReleased.Add( OnComponentReleased );
            AnchorToParent = anchorToParent;
            Offset = offset;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            AnchorToParent = Vector2.Zero;
            Offset = Vector2.Zero;
            ClearComponent();
        }

        protected override void ReleaseInternal()
        {
            base.ReleaseInternal();
            ClearComponent();
        }

        private void ClearComponent()
        {
            if( null != Component )
            {
                var temp = Component;
                Component = null;
                temp.Release();
            }
        }
    }
}
