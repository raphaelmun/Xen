using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xen3D
{
    /// <summary>
    /// Reduced version of Microsoft.Xna.Framework.IDrawable.
    /// </summary>
    public interface IDrawable3D : I3DDisplayModifiers
    {
        int DrawOrder { get; set; }
        bool Visible { get; set; }
        void Draw();
        void Draw( Matrix transformFromWorldToCamera );
    }
}
