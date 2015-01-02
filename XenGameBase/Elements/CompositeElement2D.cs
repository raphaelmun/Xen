using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public interface ICompositeElement2D : IElement2D
    {
        IPooledObjectNList<IElement2D> Children { get; }
    }

    /// <summary>
    /// An element is anything that can potentially be rendered on the screen.  Elements have behaviors that define
    /// how they interact and behave. 
    /// </summary>
    /// <example>
    /// Sprites, controls, text, layers, scenes
    /// </example>
    public abstract class CompositeElement2D<T> : ComplexElement2D<T>, ICompositeElement2D
        where T : ComposableObject, new()
    {
        //Cache these delegates so that their invocation does not generate garbage.
        private Action<IElement2D> _onAfterChildAdded;
        private Action<IElement2D> _onBeforeChildRemoved;

        private SortedElementCollection _children = new SortedElementCollection();
        
        public IPooledObjectNList<IElement2D> Children{ get { return _children; } }

        public CompositeElement2D()
        {
            _onAfterChildAdded = new Action<IElement2D>( OnAfterChildAdded );
            _onBeforeChildRemoved = new Action<IElement2D>( OnBeforeChildRemoved );
            
            _children.OnItemAfterAdded.Add( _onAfterChildAdded );
            _children.OnItemBeforeRemoved.Add( _onBeforeChildRemoved );
        }

        protected override bool IntersectsImpl( ICollidableObject collidableObject )
        {
            foreach( var child in Children.Items )
            {
                if( child.CollisionExtent.Intersects( CollisionMode, collidableObject.CollisionExtent, collidableObject.CollisionMode ) )
                    return true;
            }
            return false;
        }

        private void OnAfterChildAdded( IElement2D child )
        {
            if( null != child )
            {
                child.Parent = this;
                VisualComponents.Add( child );
                child.OpacityModifier = OpacityFinal;
            }
        }

        private void OnBeforeChildRemoved( IElement2D child )
        {
            if( null != child )
            {
                child.OpacityModifier = 1.0f;
                VisualComponents.Remove( child );
                child.Parent = null;
            }
        }

        protected override void ReleaseInternal()
        {
            base.ReleaseInternal();
            ResetDirectState();
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _children.ReleaseAndClearChildren();
            VisualComponents.Clear();
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            foreach( var child in Children.Items )
            {
                child.Draw( spriteBatch, transformFromWorldToCamera );
                if( ShowDebug )
                {
                    spriteBatch.DrawExtent( child.CollisionExtent, DebugColor, transformFromWorldToCamera );
                    spriteBatch.DrawExtent( child.RenderingExtent, DebugColor, transformFromWorldToCamera );
                }
            }
        }

        public void ProcessInput( ref InputState input, Matrix transformFromCameraToWorld )
        {
            if( WantsInput && Enabled )
            {
                base.ProcessInput( ref input, transformFromCameraToWorld );
                ProcessInputInternal( ref input, transformFromCameraToWorld );
            }
        }

        protected virtual void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            //Iterate backwards because the items drawn first are the lowest in the z-order
            for( int i = Children.Items.Count - 1; i >= 0; i-- )
                Children.Items[ i ].ProcessInput( ref input, transformFromCameraToWorld );
        }

        internal void DrawExtents( SpriteBatch spriteBatch, Color lineColor, Matrix transformFromWorldToCamera )
        {
            base.DrawExtents( spriteBatch, lineColor, transformFromWorldToCamera );
            foreach( var child in Children.Items )
            {
                spriteBatch.DrawExtent( child.CollisionExtent, lineColor, transformFromWorldToCamera );
                spriteBatch.DrawExtent( child.RenderingExtent, lineColor, transformFromWorldToCamera );
            }
        }

        protected override void OnOpacityChanged()
        {
            base.OnOpacityChanged();
            foreach( var child in Children.Items )
                child.OpacityModifier = OpacityFinal;
        }
    }
}
