using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public interface IElement2D : IRenderable2D, IPooledObject, IAcceptsInput, ICollidableObject
    {
        ISortedPooledObjectNList<IBehavior> Behaviors { get; }
        ISortedDecoratorCollection Decorators { get; }
        ICompositeElement2D Parent { get; set; }

        void DetachFromParent();
        void AttachToParent( ICompositeElement2D parent );
    }

    /// <summary>
    /// An element is anything that can potentially be rendered on the screen.  Elements have behaviors that define
    /// how they interact and behave. 
    /// </summary>
    /// 
    /// <example>
    /// Sprites, controls, text, layers, scenes
    /// </example>
    public abstract class Element2D<T> : Renderable2DBase<T>, IElement2D
        where T : ComposableObject, new()
    {
        //TODO: look for a way to merge these two collections as this prevents interleving of draw orders.
        private SortedBehaviorCollection _behaviors = new SortedBehaviorCollection();
        private SortedDecoratorCollection _decorators = new SortedDecoratorCollection();
        private uint _collisionClass = CollisionClasses.DoesNotCollide;
        private CollisionMode _collisionMode = CollisionMode.Extent;
        private IRenderable2D _visualComponent;
        private ICompositeElement2D _parent;

        //Cache these delegates so that their invocation does not generate garbage.
        private Action<IBehavior> _onBehaviorAfterAdded;
        private Action<IBehavior> _onBehaviorAfterRemoved;
        private Action<IRenderable2D> _onAfterVisualElementAdded;
        private Action<IRenderable2D> _onBeforeVisualElemenRemoved;

        public ISortedPooledObjectNList<IBehavior> Behaviors { get { return _behaviors; } }
        public ISortedDecoratorCollection Decorators { get { return _decorators; } }
        public List<IBehavior> BehaviorItems { get { return _behaviors.Items; } }

        protected virtual IRenderable2D VisualComponent
        {
            get { return _visualComponent; }
            set { _visualComponent = value; }
        }

        public uint CollisionClass
        {
            get { return _collisionClass; }
            protected set { _collisionClass = value; }
        }

        public virtual IExtent CollisionExtent 
        {
            get { return RenderingExtent; }
        }

        public CollisionMode CollisionMode
        {
            get { return _collisionMode; }
            protected set { _collisionMode = value; }
        }

        public bool IsCollidable
        {
            get 
            { 
                return ( CollisionClass != CollisionClasses.DoesNotCollide ) && 
                       ( CollisionExtent != null ) && 
                       ( CollisionMode != CollisionMode.None ); 
            }
        }

        public ICompositeElement2D Parent 
        {
            get { return _parent; }
            set
            {
                if( null != value )
                    AttachToParent( value );
                else
                    DetachFromParent();
            }
        }

        protected override IExtent PhysicalExtent { get { return CollisionExtent; } }

        public override IRectangularExtent RenderingExtent { get { return _visualComponent.RenderingExtent; } }

        public bool WantsInput { get { return true; } }
        public Color DebugColor { get; set; }
        public bool ShowDebug { get; set; }

        #region I2DDisplayModifiers redirects to VisualElement
        public override float LayerDepth 
        {
            get { return VisualComponent.LayerDepth; }
            set { VisualComponent.LayerDepth = value; }
        }

        public override Color ModulationColor 
        { 
            get { return VisualComponent.ModulationColor; }
            set { VisualComponent.ModulationColor = value; }
        }

        public override Color ModulationColorWithOpacity { get { return VisualComponent.ModulationColorWithOpacity; } }

        public override float Opacity 
        {
            get { return VisualComponent.Opacity; }
            set { VisualComponent.Opacity = value; }
        }

        public override float OpacityModifier 
        {
            get { return VisualComponent.OpacityModifier; }
            set { VisualComponent.OpacityModifier = value; }
        }

        public override float OpacityFinal 
        {
            get { return VisualComponent.OpacityFinal; }
        }
        
        public override SpriteEffects SpriteEffects 
        {
            get { return VisualComponent.SpriteEffects; }
            set { VisualComponent.SpriteEffects = value; }
        }
        #endregion

        public Element2D()
        {
            _onBehaviorAfterAdded = new Action<IBehavior>( OnBehaviorAfterAdded );
            _onBehaviorAfterRemoved = new Action<IBehavior>( OnBehaviorAfterRemoved );
            _behaviors.OnItemAfterAdded.Add( _onBehaviorAfterAdded );
            _behaviors.OnItemAfterRemoved.Add( _onBehaviorAfterRemoved );

            DebugColor = Color.GreenYellow;
            ShowDebug = false;
        }

        public void AttachToParent( ICompositeElement2D parent )
        {
            if( parent != Parent )
            {
                //only set parent if it is different from current parent
                if( Parent != null )
                    throw new InvalidOperationException( "Cannot set the parent of an attached element.  Call DetachFromParent first." );
                else
                {
                    //current parent is null and new parent is not
                    if( !parent.Children.Contains( this ) )
                        parent.Children.Add( this );
                    _parent = parent;
                }
            }
        }

        public void DetachFromParent()
        {
            if( null != Parent )
            {
                //Need to set _parent to null before calling remove or else parent will try to set Child.Parent = null when it is removed.
                //This is to avoid stack overflow.  
                var p = Parent;
                _parent = null;
                if( p.Children.Contains( this ) )
                    p.Children.Remove( this );

                //Apply the parent's transform to that this element retains all the changes made to it.  Otherwise it would lose context of
                //all the transforms that had been applied to it via parent.
                Vector2 anchor = p.Transform.TranslateVectorFromThisSpaceToAbsolute( RenderingExtent.Anchor );
                RenderingExtent.Anchor = anchor;
                RenderingExtent.Angle += p.RenderingExtent.Angle;
                RenderingExtent.Scale *= p.RenderingExtent.Scale;
            }
        }

        public bool Intersects( ICollidableObject collidableObject )
        {
            if( null == collidableObject || null == CollisionExtent )
                return false;

            return IntersectsImpl( collidableObject );
        }

        protected virtual bool IntersectsImpl( ICollidableObject collidableObject )
        {
            ICompositeElement2D composite = collidableObject as ICompositeElement2D;
            if( null != composite )
            {
                return composite.Intersects( this );
            }
            else
            {
                return CollisionExtent.Intersects( CollisionMode, collidableObject.CollisionExtent, collidableObject.CollisionMode );
            }
        }
        
        public void ProcessInput( ref InputState input, Matrix transformFromCameraToWorld )
        {
            if( WantsInput && Enabled )
            {
                foreach( IBehavior behavior in BehaviorItems )
                    behavior.ProcessInput( ref input, transformFromCameraToWorld );

                ProcessInputInternal( ref input, transformFromCameraToWorld );
            }
        }

        /// <summary>
        /// Derived classes can override this method to perform custom input handling.
        /// </summary>
        /// <param name="input">The input state</param>
        protected virtual void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld ) { }

        /// <summary>
        /// Derived classes must call base.DrawInternal to draw associated behaviors
        /// </summary>
        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            DrawVisualElements( spriteBatch, transformFromWorldToCamera );
            DrawBehaviors( spriteBatch, transformFromWorldToCamera );
            if( ShowDebug )
            {
                DrawExtents( spriteBatch, DebugColor, transformFromWorldToCamera );
            }
            DrawDecorators( spriteBatch, transformFromWorldToCamera );
        }

        internal void DrawExtents( SpriteBatch spriteBatch, Color lineColor, Matrix transformFromWorldToCamera )
        {
            spriteBatch.DrawExtent( VisualComponent.RenderingExtent, lineColor, transformFromWorldToCamera );
        }

        protected virtual void DrawDecorators( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            Decorators.BeginEnumeration();

            //_marker.RenderingExtent.Anchor = Vector2.Transform( new Vector2( 100, 100 ), RenderingExtent.TranslateFrom * transformFromWorldToCamera );
            foreach( IElementDecorator decorator in Decorators.Items )
            {
                decorator.Component.RenderingExtent.Anchor 
                    = Vector2.Transform( decorator.AnchorToParent, RenderingExtent.TranslateFrom * transformFromWorldToCamera ) + decorator.Offset;
                decorator.Component.Draw( spriteBatch );
            }

            Decorators.EndEnumeration();
        }

        protected virtual void DrawBehaviors( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            Behaviors.BeginEnumeration();

            foreach( IBehavior behavior in BehaviorItems )
                behavior.Draw( spriteBatch, transformFromWorldToCamera );

            Behaviors.EndEnumeration();
        }

        protected virtual void DrawVisualElements( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            _visualComponent.Draw( spriteBatch, transformFromWorldToCamera );
        }

        private void OnBehaviorAfterAdded( IBehavior behavior )
        {
            if( null != behavior )
                behavior.Parent = this;
        }

        private void OnBehaviorAfterRemoved( IBehavior behavior )
        {
            if( null != behavior )
                behavior.Parent = null;
        }

        protected override void ReleaseInternal()
        {
            base.ReleaseInternal();
            Behaviors.ReleaseAndClearChildren();
            Decorators.ReleaseAndClearChildren();
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Parent = null;
            Behaviors.ReleaseAndClearChildren();
            CollisionClass = CollisionClasses.DoesNotCollide;
            CollisionMode = CollisionMode.Extent;
        }

        protected override void ResetDisplayModifiers()
        {
            if( VisualComponent != null )
                base.ResetDisplayModifiers();
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );

            Behaviors.BeginEnumeration();

            foreach( IBehavior behavior in BehaviorItems )
                behavior.Update( gameTime );

            Behaviors.EndEnumeration();

            Decorators.BeginEnumeration();

            foreach( IElementDecorator decorator in Decorators.Items )
                decorator.Component.Update( gameTime );

            Decorators.EndEnumeration();

            if( null != _visualComponent )
                _visualComponent.Update( gameTime );
        }

        protected override void OnOpacityChanged()
        {
            base.OnOpacityChanged();
            _visualComponent.OpacityModifier = OpacityFinal;
        }
    }

    /// <summary>
    /// An element is anything that can potentially be rendered on the screen.  Elements have behaviors that define
    /// how they interact and behave. 
    /// </summary>
    /// 
    /// <example>
    /// Sprites, controls, text, layers, scenes
    /// </example>
    public abstract class ComplexElement2D<T> : Element2D<T>, IElement2D
        where T : ComposableObject, new()
    {
        protected new ICompositeRenderable2D VisualComponents { get; set; }

        public ComplexElement2D()
        {
            VisualComponents = new CompositeRenderable2D();
            VisualComponent = VisualComponents; 
        }
    }
}
