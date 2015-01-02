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
    public class SetAnchorPositionBehavior : BehaviorBase<SetAnchorPositionBehavior>
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

#if SURFACE
        public static SetAnchorPositionBehavior Acquire( IRenderable2D target, ContactTarget contactTarget )
        {
            var instance = _pool.Acquire();
            instance.Reset( target, contactTarget );
            return instance;
        }

        public void Reset( IRenderable2D target, ContactTarget contactTarget )
        {
            _target = target;
            _contactTarget = contactTarget;
            _isFirstSetOfContacts = true;
        }
#else
        public static SetAnchorPositionBehavior Acquire( IRenderable2D target )
        {
            var instance = _pool.Acquire();
            instance.Reset( target );
            return instance;
        }

        public void Reset( IRenderable2D target )
        {
            base.Reset();
            _target = target;
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

            if( !input.MouseProcessed && input.LeftButtonPressed )
            {
                _target.RenderingExtent.Anchor = input.CurrentMousePosition;
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
                        _target.RenderingExtent.Anchor = contactLocation;
                    }
                    else
                    {
                        // Send ContactUpdate
                        _target.RenderingExtent.Anchor = contactLocation;
                    }
                }
                // ContactUp means currentContacts is missing contacts
                for( int i = 0; i < _contactsPressed.Count; i++ )
                {
                    Vector2 contactLocation = new Vector2( _contactsPressed[ i ].X, _contactsPressed[ i ].Y );
                    if( !currentContacts.Contains( _contactsPressed[ i ].Id ) )
                    {
                    }
                }
#else
#if !SILVERLIGHT
                TouchCollection currentContacts = TouchPanel.GetState();
                // ContactDown means currentContacts has new contacts
                for( int i = 0; i < currentContacts.Count; i++ )
                {
                    if( currentContacts[ i ].State == TouchLocationState.Pressed ||
                        currentContacts[ i ].State == TouchLocationState.Moved )
                    {
                        _target.RenderingExtent.Anchor = currentContacts[ i ].Position;
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
