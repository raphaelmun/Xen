using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public interface ILayer : ICompositeElement2D, IPooledObject
    {
        /// <summary>
        /// Gets the viewport for this layer, which can be manipulated to apply a transform to all entities within the layer.  
        /// </summary>
        IViewport2D Viewport { get; }

        /// <summary>
        /// Event that gets fired whenever a collision is detected amongst children within this layer.
        /// </summary>
        Event<CollisionEventArgs> OnCollision { get; }

        /// <summary>
        /// The collision rules for this layer.
        /// </summary>
        ICollisionRuleSet CollisionRules { get; set; }

        /// <summary>
        /// A collection of simple IRenderable2D elements in the layer.  These items will get Updated and Drawn but will not handle input 
        /// or participate in collision.  Use this layer for things like particles, text, and other entities that the player does not 
        /// interact with in the game world.  
        /// </summary>
        ISortedPooledObjectNList<IRenderable2D> SimpleRenderables { get; }
    }

    /// <summary>
    /// A layer represents a set of related elements that can interact with one another.  
    /// </summary>
    /// <example>
    /// -Actual game level
    /// -UI Layer
    /// -Popup dialogs
    /// -Pause menu
    /// </example>
    public class Layer : CompositeElement2D<Layer>, ILayer
    {
        public static Layer Acquire( int drawOrder )
        {
            var instance = Acquire();
            instance.DrawOrder = drawOrder;
            return instance;
        }

        private IViewport2D _viewport = Viewport2D.Acquire();
        private ICollisionDetector _collisionDetector = new SweepAndPruneCollisionDetector();
        private PerformanceTimer _updatePerformanceTimer, _drawPerformanceTimer;
        private Action<IElement2D> _onAfterItemAdded;
        private Action<IElement2D> _onBeforeItemRemoved;
        private SortedRenderable2DCollection _simpleRenderables = new SortedRenderable2DCollection();

        protected PerformanceTimer UpdatePerformance { get { return _updatePerformanceTimer; } }
        protected PerformanceTimer DrawPerformance { get { return _drawPerformanceTimer; } }

        public Event<CollisionEventArgs> OnCollision { get { return _collisionDetector.OnCollision; } }
        public IViewport2D Viewport { get { return _viewport; } }
        public Boolean IsCheckingCollisions { get; set; }
        public ISortedPooledObjectNList<IRenderable2D> SimpleRenderables { get { return _simpleRenderables; } }

        public ICollisionRuleSet CollisionRules
        {
            get { return _collisionDetector.CollisionRules; }
            set { _collisionDetector.CollisionRules = value; }
        }

        public Layer()
        {
            IsCheckingCollisions = true;
            _onAfterItemAdded = new Action<IElement2D>( OnAfterItemAdded );
            _onBeforeItemRemoved = new Action<IElement2D>( OnBeforeItemRemoved );
            Children.OnItemAfterAdded.Add( _onAfterItemAdded );
            Children.OnItemBeforeRemoved.Add( _onBeforeItemRemoved );
            _updatePerformanceTimer = new PerformanceTimer();
            _drawPerformanceTimer = new PerformanceTimer();
        }

        public void CheckCollision()
        {
            _collisionDetector.CheckCollisions();
        }

        public override void Reset()
        {
            _viewport.Reset(
                (float)Globals.Graphics.PreferredBackBufferWidth,
                (float)Globals.Graphics.PreferredBackBufferHeight );
            _viewport.ReAnchor( _viewport.ActualCenter );
            base.Reset();
        }

        private void OnAfterItemAdded( IElement2D element )
        {
            _collisionDetector.CollisionObjects.Add( element );
        }

        private void OnBeforeItemRemoved( IElement2D element )
        {
            _collisionDetector.CollisionObjects.Remove( element );
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            _drawPerformanceTimer.Begin();
            base.DrawInternal( spriteBatch, transformFromWorldToCamera * Viewport.TranslateTo );

            foreach( var renderable in SimpleRenderables.Items )
                renderable.Draw( spriteBatch, transformFromWorldToCamera * Viewport.TranslateTo );

            if( ShowDebug )
            {
                DrawExtents( spriteBatch, DebugColor, transformFromWorldToCamera * Viewport.TranslateTo );
            }

            _drawPerformanceTimer.End();
            _drawPerformanceTimer.Refresh();
        }

        internal void DrawExtents( SpriteBatch spriteBatch, Color lineColor, Matrix transformFromWorldToCamera )
        {
            base.DrawExtents( spriteBatch, lineColor, transformFromWorldToCamera * Viewport.TranslateTo );
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            _updatePerformanceTimer.Begin();
            base.UpdateInternal( gameTime );
            
            if( IsCheckingCollisions )
                _collisionDetector.CheckCollisions();

            SimpleRenderables.BeginEnumeration();
            
            foreach( var renderable in SimpleRenderables.Items )
                renderable.Update( gameTime );

            SimpleRenderables.EndEnumeration();

            _updatePerformanceTimer.End();
            _updatePerformanceTimer.Refresh();
        }
    }
}
