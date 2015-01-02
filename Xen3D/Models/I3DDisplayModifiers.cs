using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;

namespace Xen3D
{
    public interface I3DDisplayModifiers
    {
        //TODO: what is the relationship between LayerDepth and DrawOrder?
        //float LayerDepth { get; set; }
        Color ModulationColor { get; set; }
        Color ModulationColorWithOpacity { get; }
        float Opacity { get; set; }
        float OpacityModifier { get; set; }
        float OpacityFinal { get; }
    }
}
