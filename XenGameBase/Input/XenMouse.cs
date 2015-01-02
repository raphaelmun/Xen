using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public class XenMouse : Renderable2DBase<XenMouse>
    {
        private MouseState _lastState = new MouseState();

        /// <summary>
        /// The sprite to draw for the mouse cursor.  The cursor point is always the sprite's anchor point.
        /// </summary>
        private ISprite _cursorSprite;

        public Event<Vector2> OnMouseMove { get; private set; }
        public Event<Vector2> OnLeftMouseReleased { get; private set; }
        public Event<Vector2> OnRightMouseReleased { get; private set; }
        public Event<Vector2> OnLeftMousePressed { get; private set; }
        public Event<Vector2> OnRightMousePressed { get; private set; }
        public Event<Vector2> OnLeftMouseDragged { get; private set; }
        public Event<Vector2> OnRightMouseDragged { get; private set; }

        public Vector2 Position { get; private set; }

        public XenMouse()
        {
            OnMouseMove = new Event<Vector2>();
            OnLeftMouseReleased = new Event<Vector2>();
            OnRightMouseReleased = new Event<Vector2>();
            OnLeftMousePressed = new Event<Vector2>();
            OnRightMousePressed = new Event<Vector2>();
            OnLeftMouseDragged = new Event<Vector2>();
            OnRightMouseDragged = new Event<Vector2>();
        }

        /// <summary>
        /// Resets this instance to use the specified sprite as the cursor sprite.
        /// </summary>
        /// <param name="cursorSprite">The cursor sprite to use.  The cursor point will always be the 
        /// cursor sprite's anchor.</param>
        public void Reset( ISprite cursorSprite )
        {
            if( null != _cursorSprite )
            {
                _cursorSprite.Release();
            }
            _cursorSprite = cursorSprite;
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            //Ignore the camera transform since the mouse should always be drawn in the absolute coordinate space.
            if( null != _cursorSprite )
                _cursorSprite.Draw( spriteBatch );
        }

        //Template methods: derived classes can override these to perform custom actions
        protected virtual void OnMouseMoveInternal() { }
        protected virtual void OnLeftMouseReleasedInternal() { }
        protected virtual void OnRightMouseReleasedInternal() { }
        protected virtual void OnLeftMousePressedInternal() { }
        protected virtual void OnRightMousePressedInternal() { }
        protected virtual void OnLeftMouseDraggedInternal() { }
        protected virtual void OnRightMouseDraggedInternal() { }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );

            MouseState currentState = Mouse.GetState();
            Position = new Vector2( currentState.X, currentState.Y );

            if( null != _cursorSprite )
            {
                _cursorSprite.Update( gameTime );
                _cursorSprite.RenderingExtent.Anchor = Position;
            }

            if( ( _lastState.X != currentState.X ) || ( _lastState.Y != currentState.Y ) )
            {
                OnMouseMove.Notify( Position );
                OnMouseMoveInternal();
            }

            if( ( _lastState.RightButton == ButtonState.Pressed ) && ( currentState.RightButton == ButtonState.Released ) )
            {
                OnRightMouseReleased.Notify( Position );
                OnRightMouseReleasedInternal();
            }

            if( ( _lastState.LeftButton == ButtonState.Pressed ) && ( currentState.LeftButton == ButtonState.Released ) )
            {
                OnLeftMouseReleased.Notify( Position );
                OnLeftMouseReleasedInternal();
            }

            if( ( _lastState.RightButton == ButtonState.Pressed ) && ( currentState.RightButton == ButtonState.Pressed ) )
            {
                OnRightMouseDragged.Notify( Position );
                OnRightMouseDraggedInternal();
            }

            if( ( _lastState.LeftButton == ButtonState.Pressed ) && ( currentState.LeftButton == ButtonState.Pressed ) )
            {
                OnLeftMouseDragged.Notify( Position );
                OnLeftMouseDraggedInternal();
            }

            if( ( _lastState.LeftButton == ButtonState.Released ) && ( currentState.LeftButton == ButtonState.Pressed ) )
            {
                OnLeftMousePressed.Notify( Position );
                OnLeftMousePressedInternal();
            }

            if( ( _lastState.RightButton == ButtonState.Released ) && ( currentState.RightButton == ButtonState.Pressed ) )
            {
                OnRightMousePressed.Notify( Position );
                OnRightMousePressedInternal();
            }

            _lastState = currentState;
        }
    }
}
