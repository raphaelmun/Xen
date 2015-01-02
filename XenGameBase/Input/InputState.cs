using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XenGameBase
{
    public struct InputState
    {
        public MouseState CurrentMouseState;
        public KeyboardState CurrentKeyboardState;
        public bool MouseProcessed;
        public bool KeyboardProcessed;

        public MouseState LastMouseState;
        public KeyboardState LastKeyboardState;

        public InputState( 
            MouseState currentMouseState, 
            MouseState lastMouseState, 
            KeyboardState currentKeyboardState, 
            KeyboardState lastKeyboardState )
        {
            CurrentMouseState = currentMouseState;
            LastMouseState = lastMouseState;
            CurrentKeyboardState = currentKeyboardState;
            LastKeyboardState = lastKeyboardState;
            MouseProcessed = false;
            KeyboardProcessed = false;
        }

        public Vector2 CurrentMousePosition
        {
            get { return new Vector2( CurrentMouseState.X, CurrentMouseState.Y ); }
        }

        public bool LeftButtonPressed
        {
            get
            {
                return ( LastMouseState.LeftButton == ButtonState.Released ) &&
                       ( CurrentMouseState.LeftButton == ButtonState.Pressed );
            }
        }

        public bool RightButtonPressed
        {
            get
            {
                return ( LastMouseState.RightButton == ButtonState.Released ) &&
                       ( CurrentMouseState.RightButton == ButtonState.Pressed );
            }
        }

        public int ScrollWheelValueDelta
        {
            get
            {
#if SILVERLIGHT
                return 0; // TODO: Is there a way to get mouse wheel value in silverlight?
#else
                return CurrentMouseState.ScrollWheelValue - LastMouseState.ScrollWheelValue;
#endif
            }
        }

        public bool IsKeyPressed( Keys key )
        {
            return LastKeyboardState.IsKeyUp( key ) && CurrentKeyboardState.IsKeyDown( key );
        }
    }
}
