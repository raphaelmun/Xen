using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xen2D;
using XenGameBase;
#if SURFACE
using Microsoft.Surface.Core;
#else
#if !SILVERLIGHT
using Microsoft.Xna.Framework.Input.Touch;
#endif
#endif

namespace GarageDemo
{
    public class ToggleLayerBehavior : BehaviorBase<ToggleLayerBehavior>
    {
#if SURFACE
        private ReadOnlyContactCollection _contactsPressed;
        private ContactTarget _contactTarget;
#else
#if !SILVERLIGHT
        TouchCollection _contactsPressed;
#endif
#endif
        private bool _isFirstSetOfContacts = true;

        private IRenderable2D _target;
        private Layer _layer;

#if SURFACE
        public static ToggleLayerBehavior Acquire( IRenderable2D target, Layer layer, ContactTarget contactTarget )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, layer, contactTarget );
            return instance;
        }

        public void Reset( IRenderable2D target, Layer layer, ContactTarget contactTarget )
        {
            _target = target;
            _layer = layer;
            _contactTarget = contactTarget;
            if( _layer.Enabled )
            {
                _target.Opacity = 1.0f;
            }
            else
            {
                _target.Opacity = 0.25f;
            }
            _isFirstSetOfContacts = true;
        }
#else
        public static ToggleLayerBehavior Acquire( IRenderable2D target, Layer layer )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, layer );
            return instance;
        }

        public void Reset( IRenderable2D target, Layer layer )
        {
            _target = target;
            _layer = layer;
            if( _layer.Enabled )
            {
                _target.Opacity = 1.0f;
            }
            else
            {
                _target.Opacity = 0.25f;
            }
            _isFirstSetOfContacts = true;
        }
#endif

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            Visible = false;
            _target = null;
            WantsInput = true;
        }

        protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld )
        {
            base.ProcessInputInternal( ref input, transformFromCameraToWorld );

            if( !input.MouseProcessed )
            {
                if( _target.RenderingExtent.ContainsPoint( input.CurrentMousePosition ) &&
                    input.LeftButtonPressed )
                {
                    _layer.Enabled = !_layer.Enabled;
                    _layer.Visible = !_layer.Visible;

                    if( _layer.Enabled )
                    {
                        _target.Opacity = 1.0f;
                    }
                    else
                    {
                        _target.Opacity = 0.25f;
                    }
                }
            }

            if( _isFirstSetOfContacts )
            {
#if SURFACE
                _contactsPressed = _contactTarget.GetState();
#else
#if !SILVERLIGHT
                _contactsPressed = TouchPanel.GetState();
#endif
#endif
                _isFirstSetOfContacts = false;
            }
            else
            {
#if SURFACE
                ReadOnlyContactCollection currentContacts = _contactTarget.GetState();
                // ContactDown means currentContacts has new contacts
                for( int i = 0; i < currentContacts.Count; i++ )
                {
                    Vector2 contactLocation = new Vector2( currentContacts[ i ].X, currentContacts[ i ].Y );
                    if( !_contactsPressed.Contains( currentContacts[ i ].Id ) )
                    {
                        // Send ContactDown
                    }
                    else
                    {
                        // Send ContactUpdate
                    }
                }
                // ContactUp means currentContacts is missing contacts
                for( int i = 0; i < _contactsPressed.Count; i++ )
                {
                    Vector2 contactLocation = new Vector2( _contactsPressed[ i ].X, _contactsPressed[ i ].Y );
                    if( !currentContacts.Contains( _contactsPressed[ i ].Id ) )
                    {
                        if( _target.RenderingExtent.ContainsPoint( contactLocation ) )
                        {
                            _layer.Enabled = !_layer.Enabled;
                            _layer.Visible = !_layer.Visible;

                            if( _layer.Enabled )
                            {
                                _target.Opacity = 1.0f;
                            }
                            else
                            {
                                _target.Opacity = 0.25f;
                            }
                        }
                    }
                }
#else
#if !SILVERLIGHT
                TouchCollection currentContacts = TouchPanel.GetState();
                // ContactDown means currentContacts has new contacts
                for( int i = 0; i < currentContacts.Count; i++ )
                {
                    if( currentContacts[ i ].State == TouchLocationState.Released )
                    {
                        if( _target.RenderingExtent.ContainsPoint( currentContacts[i].Position ) )
                        {
                            _layer.Enabled = !_layer.Enabled;
                            _layer.Visible = !_layer.Visible;

                            if( _layer.Enabled )
                            {
                                _target.Opacity = 1.0f;
                            }
                            else
                            {
                                _target.Opacity = 0.25f;
                            }
                        }
                    }
                }
#endif
#endif
                
#if !SILVERLIGHT
                _contactsPressed = currentContacts;
#endif
            }
        }
    }
}
