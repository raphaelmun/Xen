using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenGameBase;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace XenFarseer
{
    interface IXenFarseerBehavior
    {
        //bool IsOneTimeBehavior { get; }
        void RunBehavior( Body body, GameTime gameTime );
    }
}
