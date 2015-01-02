
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;
using Xen2D;

namespace Xen3D
{
    public abstract class Component3D<T> : RenderableBase<T>, IComponent3D
        where T : ComposableObject, new()
    {
        private Color _modulationColor;
        private Color _modulationColorWithOpacity;
        private float _opacity;
        private float _opacityModifier = 1.0f;

        public virtual int DrawOrder { get; set; }

        public virtual Color ModulationColor
        {
            get { return _modulationColor; }
            set
            {
                _modulationColor = value;
                UpdateModulationColor();
            }
        }

        public virtual Color ModulationColorWithOpacity
        {
            get { return _modulationColorWithOpacity; }
        }

        public virtual float Opacity
        {
            get { return _opacity; }
            set
            {
                if( _opacity != value )
                {
                    _opacity = value;
                    UpdateModulationColor();
                    OnOpacityChanged();
                }
            }
        }

        /// <summary>
        /// The modifier to apply the opacity.  This value can be used to indicate parent element opacity
        /// </summary>
        public virtual float OpacityModifier
        {
            get { return _opacityModifier; }
            set
            {
                if( value != _opacityModifier )
                {
                    _opacityModifier = value;
                    UpdateModulationColor();
                    OnOpacityChanged();
                }
            }
        }

        /// <summary>
        /// Returns the product of the opacity and its modifier
        /// </summary>
        public virtual float OpacityFinal
        {
            get { return _opacity * _opacityModifier; }
        }

        /// <summary>
        /// The sorting depth of the sprite, between 0 (front) and 1 (back)
        /// </summary>
        public virtual float LayerDepth{ get; set;}

        public void Draw()
        {
            Draw( Matrix.Identity );
        }

        public void Draw(  Matrix transformFromWorldToCamera )
        {
            if( Visible )
            {
                DrawInternal( transformFromWorldToCamera );
            }
        }

        protected abstract void DrawInternal( Matrix transformFromWorldToCamera );

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            ResetDisplayModifiers();
        }

        protected virtual void ResetDisplayModifiers()
        {
            //SpriteDisplayAttributeDefaults.SetDefaults( this );
        }

        private void UpdateModulationColor()
        {
            _modulationColorWithOpacity = _modulationColor * OpacityFinal; ;
        }

        protected virtual void OnOpacityChanged() { }
    }
}