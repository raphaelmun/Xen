using XenAspects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xen2D;

namespace Xen3D
{
    /// <summary>
    /// This class serves as the base for any 3D entity that renders/updates as 2.5D
    /// </summary>
    /// <typeparam name="T">The parameterized type.</typeparam>
    public abstract class Renderable3DBase<T> : Component3D<T>, IRenderable3D
        where T : ComposableObject, new()
    {
        public const int DefaultDrawOrder = 0;

        private RectangularExtent _renderingExtent = new RectangularExtent();
        private PositionTimeHistory2D _history = new PositionTimeHistory2D();

        public IPositionTimeHistory2D History { get { return _history; } }

        protected virtual IExtent PhysicalExtent { get { return RenderingExtent; } }

        public virtual IRectangularExtent RenderingExtent
        {
            get { return _renderingExtent; }
        }

        public Space2DTranslation Transform
        {
            get { return RenderingExtent.Transform; }
            set { RenderingExtent.Transform = value; }
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            _history.Reset();
            _renderingExtent.Reset();
            DrawOrder = DefaultDrawOrder;
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );
            Vector2 transformedAnchor = Vector2.Transform( PhysicalExtent.Origin, PhysicalExtent.TranslateFrom );
            History.Add( transformedAnchor, PhysicalExtent.Angle, gameTime.TotalGameTime );
        }
    }
}
