using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenAspects;

namespace XenGameBase
{
    public struct XenMouseState
    {
        public bool LeftClickProcessed;
        public bool RightClickProcessed;
        public bool MiddleClickProceesed;
        public bool MouseOverProcessed;
        public MouseState State;

        public XenMouseState( MouseState state )
        {
            LeftClickProcessed = false;
            RightClickProcessed = false;
            MiddleClickProceesed = false;
            MouseOverProcessed = false;
            State = state;
        }
    }
}
