using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xen3D
{
    public interface IModelInfo
    {
        Model Asset { get; }
        Matrix SourceTransform { get; }
        Vector2 PixelSize { get; }
    }
}
