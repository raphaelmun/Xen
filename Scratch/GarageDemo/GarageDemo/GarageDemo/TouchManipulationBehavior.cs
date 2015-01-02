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
    public class TouchManipulationBehavior : BehaviorBase<TouchManipulationBehavior>
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
        private int _firstContactID = -1, _secondContactID = -1;
        private Vector2 _referenceOrientationVector = Vector2.Zero;
        private float _referenceOrientationAngle = 0.0f;
        private Vector2 _referenceScale = Vector2.One;

        private IRenderable2D _target;

#if SURFACE
        public static TouchManipulationBehavior Acquire( IRenderable2D target, ContactTarget contactTarget )
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
#if !SILVERLIGHT
        public static TouchManipulationBehavior Acquire( IRenderable2D target )
        {
            var instance = _pool.Acquire();
            instance.Reset( target );
            return instance;
        }

        public void Reset( IRenderable2D target )
        {
            _target = target;
            _isFirstSetOfContacts = true;
        }
#endif
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
                        if( _firstContactID == -1 )
                        {
                            // First Contact - Controls Position and Origin
                            // Check if the contact location is within the extent
                            if( _target.RenderingExtent.ContainsPoint( contactLocation ) )
                            {
                                _firstContactID = currentContacts[ i ].Id;
                                // Set the Origin to the contact
                                //_target.RenderingExtent.Origin = contactLocation;
                                _target.RenderingExtent.ReAnchor( contactLocation );
                            }
                        }
                        else if( _secondContactID == -1 )
                        {
                            // Second Contact - Controls Orientation and Scale
                            if( _target.RenderingExtent.ContainsPoint( contactLocation ) )
                            {
                                _secondContactID = currentContacts[ i ].Id;
                                // Set the reference vector
                                //_referenceOrientationVector = contactLocation - _target.RenderingExtent.Origin;
                                _referenceOrientationVector = contactLocation - _target.RenderingExtent.Anchor;
                                _referenceOrientationAngle = _target.RenderingExtent.Angle;
                                _referenceScale = _target.RenderingExtent.Scale;
                            }
                        }
                        else
                        {
                            // Additional Contacts
                        }
                    }
                    else
                    {
                        // Send ContactUpdate
                        if( _firstContactID == currentContacts[ i ].Id )
                        {
                            // First Contact - Controls Position and Origin
                            Vector2 moveVector = contactLocation - _target.RenderingExtent.Anchor;
                            // Update the anchor and origin
                            _target.RenderingExtent.Anchor += moveVector;
                            //_target.RenderingExtent.Origin = contactLocation;
                            _target.RenderingExtent.ReAnchor( contactLocation );
                        }
                        else if( _secondContactID == currentContacts[ i ].Id )
                        {
                            // Second Contact - Controls Orientation and Scale
                            //Vector2 vector = contactLocation - _target.RenderingExtent.Origin;
                            Vector2 vector = contactLocation - _target.RenderingExtent.Anchor;
                            Vector2 scaleFactor = vector.Length() / _referenceOrientationVector.Length() * _referenceScale;
                            float angleDifference = (float)XenMath.GetDifferenceAngleFloat( vector, _referenceOrientationVector );
                            _target.RenderingExtent.Angle = _referenceOrientationAngle + angleDifference;
                            _target.RenderingExtent.Scale = scaleFactor;
                        }
                        else
                        {
                            // Additional Contacts
                        }
                    }
                }
                // ContactUp means currentContacts is missing contacts
                for( int i = 0; i < _contactsPressed.Count; i++ )
                {
                    if( !currentContacts.Contains( _contactsPressed[ i ].Id ) )
                    {
                        if( _firstContactID == _contactsPressed[ i ].Id )
                        {
                            // Invalidate both
                            _firstContactID = -1;
                            _secondContactID = -1;
                        }
                        else if( _secondContactID == _contactsPressed[ i ].Id )
                        {
                            _secondContactID = -1;
                        }
                    }
                }

                _contactsPressed = currentContacts;
            }
#else
#if !SILVERLIGHT
                TouchCollection currentContacts = TouchPanel.GetState();
                // ContactDown means currentContacts has new contacts
                for( int i = 0; i < currentContacts.Count; i++ )
                {
                    if( currentContacts[ i ].State == TouchLocationState.Pressed ||
                        currentContacts[i].State == TouchLocationState.Moved )
                    {
                        // Send ContactDown
                        if( _firstContactID == -1 )
                        {
                            // First Contact - Controls Position and Origin
                            // Check if the contact location is within the extent
                            if( _target.RenderingExtent.ContainsPoint( currentContacts[ i ].Position ) )
                            {
                                _firstContactID = currentContacts[ i ].Id;
                                // Set the Origin to the contact
                                //_target.RenderingExtent.Origin = contactLocation;
                                _target.RenderingExtent.ReAnchor( currentContacts[ i ].Position );
                            }
                        }
                        else if( _firstContactID != currentContacts[ i ].Id && _secondContactID == -1 )
                        {
                            // Second Contact - Controls Orientation and Scale
                            if( _target.RenderingExtent.ContainsPoint( currentContacts[ i ].Position ) )
                            {
                                _secondContactID = currentContacts[ i ].Id;
                                // Set the reference vector
                                //_referenceOrientationVector = contactLocation - _target.RenderingExtent.Origin;
                                _referenceOrientationVector = currentContacts[ i ].Position - _target.RenderingExtent.Anchor;
                                _referenceOrientationAngle = _target.RenderingExtent.Angle;
                                _referenceScale = _target.RenderingExtent.Scale;
                            }
                        }
                        // Send ContactUpdate
                        else if( _firstContactID == currentContacts[ i ].Id )
                        {
                            // First Contact - Controls Position and Origin
                            Vector2 moveVector = currentContacts[ i ].Position - _target.RenderingExtent.Anchor;
                            // Update the anchor and origin
                            _target.RenderingExtent.Anchor += moveVector;
                            //_target.RenderingExtent.Origin = contactLocation;
                            _target.RenderingExtent.ReAnchor( currentContacts[ i ].Position );
                        }
                        else if( _secondContactID == currentContacts[ i ].Id )
                        {
                            // Second Contact - Controls Orientation and Scale
                            //Vector2 vector = contactLocation - _target.RenderingExtent.Origin;
                            Vector2 vector = currentContacts[ i ].Position - _target.RenderingExtent.Anchor;
                            Vector2 scaleFactor = vector.Length() / _referenceOrientationVector.Length() * _referenceScale;
                            float angleDifference = (float)XenMath.GetDifferenceAngleFloat( vector, _referenceOrientationVector );
                            _target.RenderingExtent.Angle = _referenceOrientationAngle + angleDifference;
                            _target.RenderingExtent.Scale = scaleFactor;
                        }
                        else
                        {
                            // Additional Contacts
                        }
                    }
                }

                // Search and see if either contacts exist
                // Send ContactUp
                TouchLocation contactLocation;
                if( !currentContacts.FindById( _firstContactID, out contactLocation ) )
                {
                    // Invalidate both
                    _firstContactID = -1;
                    _secondContactID = -1;
                }
                else if( !currentContacts.FindById( _secondContactID, out contactLocation ) )
                {
                    _secondContactID = -1;
                }

                _contactsPressed = currentContacts;
#endif
            }
#endif
        }
    }
}
