using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xen2D;
using XenGameBase;
using Microsoft.Xna.Framework;

namespace GarageDemo
{
    public class LayerToggleTextButtonElement : Element2D<LayerToggleTextButtonElement>
    {
        public XenString Text = null;
        public Layer Layer = null;
        private ToggleLayerBehavior _toggleBehavior = null;

        public static LayerToggleTextButtonElement Acquire( Vector2 position, string text, Layer layer )
        {
            var instance = _pool.Acquire();
            instance.Text.Reset( Fonts.Get( FontId.Arial ), text );
            instance.Text.RenderingExtent.Anchor = position;
            instance.Layer = layer;
            instance.ResetDirectState();
            return instance;
        }

        public LayerToggleTextButtonElement()
        {
            Text = XenString.Acquire();
            VisualComponent = Text;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponent = Text;
            if( Layer != null )
            {
#if SURFACE
                _toggleBehavior = ToggleLayerBehavior.Acquire( this, Layer, SurfaceGameBase.ContactTarget );
#else
                _toggleBehavior = ToggleLayerBehavior.Acquire( this, Layer );
#endif
                Behaviors.Add( _toggleBehavior );
            }
        }
    }
}
