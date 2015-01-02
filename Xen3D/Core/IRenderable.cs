using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;
using Xen2D;

namespace Xen3D
{
    /// <summary>
    /// Interface for a 2.5D component that can be updated and drawn.  
    /// </summary>
    public interface IComponent3D : IUpdateableEx, IDrawable3D, IPooledObject
    {
    }

    /// <summary>
    /// Interface for a 2.5D component that owns its own rendering extent and transform.
    /// </summary>
    public interface IRenderable3D : IComponent3D
    {
        IRectangularExtent RenderingExtent { get; }
        Space2DTranslation Transform { get; set; }
        IPositionTimeHistory2D History { get; }
    }

    public interface ICompositeRenderable3D : IRenderable3D, ISortedPooledObjectNList<IRenderable3D>
    {
        void AddExtent( IExtent extent );
    }
}
