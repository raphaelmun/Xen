using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XenGameBase;
using FarseerPhysics.Dynamics;
using Xen2D;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;

namespace XenFarseer
{
    // For Reference:
    //public interface IXenPlugin
    //{
    //    // Plugin Identifiers
    //    string Name { get; }
    //    string Description { get; }
    //    string Author { get; }
    //    string Version { get; }

    //    // Main plugin methods
    //    void Initialize();
    //    void Load();
    //    void Unload();
    //    void Update( GameTime gameTime );
    //    void Draw( GameTime gameTime );

    //    void Call( int functionalityID, params object[] parameterList );
    //}

    public enum XenFarseerFunc : int
    {
        SetScreenEdgeCollision,
        SetGravity,
        //SetElementMass,
        //SetElementVelocity,
        //SetElementAngularVelocity,
        //SetElementFriction,
        ApplyForce,
        ApplyTorque,
        AttachDistanceJoint,
        AttachSliderJoint,
    }

    public delegate void XenFarseerCollisionHandler( IElement2D elementA, IElement2D elementB, Vector2 contact );

    public class FarseerPlugin : IXenPlugin
    {
        World world;
        Dictionary<IElement2D, Fixture> elements;
        Layer gameLayer;
        Boolean isScreenEdgeCollidable;
        Fixture[] screenEdgeFixtures;

        #region Identifier Properties

        public string Name
        {
            get { return "XenFarseer"; }
        }

        public string Description
        {
            get { return "Farseer Physics Plugin for Xen"; }
        }

        public string Author
        {
            get { return "Raphael Mun"; }
        }

        public string Version
        {
            get { return "0.1"; }
        }

        public Action<IElement2D> OnItemBeforeAdded
        {
            get { return null; }
        }
        
        public Action<IElement2D> OnItemAfterAdded
        {
            get { return OnItemAdd; }
        }

        public Action<IElement2D> OnItemBeforeRemoved
        {
            get { return null; }
        }

        public Action<IElement2D> OnItemAfterRemoved
        {
            get { return OnItemRemove; }
        }

        private void OnItemAdd( IElement2D element )
        {
            Fixture fixture = null;
            IExtent extent = element.CollisionExtent;
            ICompositeExtent composite = extent as ICompositeExtent;
            Vector2 positionOverride = Vector2.Zero;
            float rotationOverride = 0.0f;
            if( null != composite )
            {
                // TODO: Temporary!!!!!
                positionOverride = composite.Anchor;
                rotationOverride = composite.Angle;
                extent = composite.Items[ 0 ];
                //throw new NotImplementedException("Composite Elements not implemented");
            }
            ICircularExtent circle = extent as ICircularExtent;
            if( null != circle )
            {
                fixture = FixtureFactory.CreateCircle( world, circle.Radius, 1.0f, extent.Anchor + positionOverride, element );
                //fixture.Body.LocalCenter = extent.ActualCenter - extent.Anchor;
                fixture.Body.Rotation = extent.Angle + rotationOverride;
                fixture.Body.UserData = element;
            }
            IPolygonExtent polygon = extent as IPolygonExtent;
            if( null != polygon )
            {
                Vertices vertices = new Vertices();
                for( int i = 0; i < polygon.NumSides; i++ )
                {
                    vertices.Add( polygon.Vertices[ i ] - positionOverride - extent.Anchor );
                }
                fixture = FixtureFactory.CreatePolygon( world, vertices, 1.0f, extent.Anchor + positionOverride, element );
                //fixture.Body.LocalCenter = extent.ActualCenter - extent.Anchor;
                fixture.Body.Rotation = extent.Angle + rotationOverride;
                fixture.Body.UserData = element;
            }
            elements.Add( element, fixture );
            fixture.OnCollision += OnCollision;
            fixture.Body.IsStatic = false; // Dynamic by default
            fixture.Body.Mass = 1.0f;

            //// TODO: This might be slow, in which case we need a better solution
            //foreach( IBehavior behavior in element.Behaviors.Items )
            //{
            //    Type type = behavior.GetType();
            //    foreach( Type typeInt in type.GetInterfaces() )
            //    {
            //        if( typeInt == typeof( IXenFarseerBehavior ) )
            //        {
            //            if( ( (IXenFarseerBehavior)behavior ).IsOneTimeBehavior )
            //            {
            //                ( (IXenFarseerBehavior)behavior ).RunBehavior( fixture.Body, new GameTime() );
            //            }
            //        }
            //    }
            //}
        }

        private void OnItemRemove( IElement2D element )
        {
            if( elements.ContainsKey( element ) )
            {
                Fixture fixture = elements[ element ];
                elements.Remove( element );
                world.RemoveBody( fixture.Body );
            }
        }

        #endregion

        #region Methods

        public void Initialize( Layer layer )
        {
            world = new World( Vector2.Zero );
            elements = new Dictionary<IElement2D, Fixture>();
            gameLayer = layer;
            gameLayer.IsCheckingCollisions = false; // Override Xen's collisions
            isScreenEdgeCollidable = true;
            screenEdgeFixtures = new Fixture[ 4 ];
        }

        public void Load()
        {
            world.Gravity = Vector2.UnitY * 98.1f;
            // Create screen-edge fixtures
            IViewport2D viewport = (IViewport2D)gameLayer.Viewport;
            screenEdgeFixtures[ 0 ] = FixtureFactory.CreateEdge( world, viewport.ActualTopLeft, viewport.ActualTopRight, null );
            screenEdgeFixtures[ 1 ] = FixtureFactory.CreateEdge( world, viewport.ActualTopLeft, viewport.ActualBottomLeft, null );
            screenEdgeFixtures[ 2 ] = FixtureFactory.CreateEdge( world, viewport.ActualTopRight, viewport.ActualBottomRight, null );
            screenEdgeFixtures[ 3 ] = FixtureFactory.CreateEdge( world, viewport.ActualBottomLeft, viewport.ActualBottomRight, null );
            for( int i = 0; i < 4; i++ )
            {
                screenEdgeFixtures[ i ].Body.IsStatic = true;
            }
        }

        public void Unload()
        {
            foreach( Fixture fixture in elements.Values )
            {
                world.RemoveBody( fixture.Body );
            }
            for( int i = 0; i < 4; i++ )
            {
                world.RemoveBody( screenEdgeFixtures[ i ].Body );
            }
            elements.Clear();
        }

        public void Update( GameTime gameTime )
        {
            // Update before the world step in case the game moved the element
            foreach( Body body in world.BodyList )
            {
                IElement2D element = (IElement2D)body.UserData;
                if( element != null )
                {
                    if( body.Position != element.CollisionExtent.Anchor ||
                        body.Rotation != element.CollisionExtent.Angle )
                    {
                        body.Position = element.CollisionExtent.Anchor;
                        body.Rotation = element.CollisionExtent.Angle;
                        body.Awake = true;
                    }
                }
            }

            world.Step( (float)gameTime.ElapsedGameTime.TotalSeconds );

            // Update the elements according to the world
            foreach( Body body in world.BodyList )
            {
                IElement2D element = (IElement2D)body.UserData;
                if( element != null )
                {
                    element.CollisionExtent.Anchor = body.Position;
                    element.CollisionExtent.Angle = body.Rotation;

                    // TODO: This might be slow, in which case we need a better solution
                    foreach( IBehavior behavior in element.Behaviors.Items )
                    {
                        Type type = behavior.GetType();
                        foreach( Type typeInt in type.GetInterfaces() )
                        {
                            if( typeInt == typeof( IXenFarseerBehavior ) )
                            {
                                //if( !( (IXenFarseerBehavior)behavior ).IsOneTimeBehavior )
                                {
                                    ( (IXenFarseerBehavior)behavior ).RunBehavior( body, gameTime );
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Draw( GameTime gameTime, SpriteBatch spriteBatch )
        { }

        public void Call( int functionalityID, params object[] parameterList )
        {
            if( parameterList.Length != numberOfParameters( functionalityID ) )
            {
                throw new ArgumentOutOfRangeException( "Parameter count does not match functionality" );
            }

            Fixture fixture = null;
            switch( functionalityID )
            {
                case (int)XenFarseerFunc.SetScreenEdgeCollision: // Params: [bool]
                    {
                        isScreenEdgeCollidable = (bool)parameterList[ 0 ];
                    }
                    break;
                case (int)XenFarseerFunc.SetGravity: // Params: [Vector2]
                    {
                        world.Gravity = (Vector2)parameterList[ 0 ];
                    }
                    break;
                //case (int)XenFarseerFunc.SetElementMass: // Params: [IElement2D] [float]
                //    {
                //        IElement2D element = (IElement2D)parameterList[ 0 ];
                //        fixture = elements[ element ];
                //        fixture.Body.IsStatic = ( (float)parameterList[ 1 ] == 0.0f );
                //        fixture.Body.Mass = (float)parameterList[ 1 ];
                //    }
                //    break;
                //case (int)XenFarseerFunc.SetElementVelocity: // Params: [IElement2D] [Vector2]
                //    {
                //        IElement2D element = (IElement2D)parameterList[ 0 ];
                //        fixture = elements[ element ];
                //        fixture.Body.LinearVelocity = (Vector2)parameterList[ 1 ];
                //    }
                //    break;
                //case (int)XenFarseerFunc.SetElementAngularVelocity: // Params: [IElement2D] [float]
                //    {
                //        IElement2D element = (IElement2D)parameterList[ 0 ];
                //        fixture = elements[ element ];
                //        fixture.Body.AngularVelocity = (float)parameterList[ 1 ];
                //    }
                //    break;
                //case (int)XenFarseerFunc.SetElementFriction: // Params: [IElement2D] [float]
                //    {
                //        IElement2D element = (IElement2D)parameterList[ 0 ];
                //        fixture = elements[ element ];
                //        fixture.Friction = (float)parameterList[ 1 ];
                //    }
                //    break;
                case (int)XenFarseerFunc.ApplyForce: // Params: [IElement2D] [Vector2]
                    {
                        IElement2D element = (IElement2D)parameterList[ 0 ];
                        fixture = elements[ element ];
                        fixture.Body.ApplyForce( (Vector2)parameterList[ 1 ] );
                    }
                    break;
                case (int)XenFarseerFunc.ApplyTorque: // Params: [IElement2D] [float]
                    {
                        IElement2D element = (IElement2D)parameterList[ 0 ];
                        fixture = elements[ element ];
                        fixture.Body.ApplyTorque( (float)parameterList[ 1 ] );
                    }
                    break;
                case (int)XenFarseerFunc.AttachDistanceJoint: // Params: [IElement2D] [IElement2D] [Vector2] [Vector2]
                    {
                        IElement2D elementA = (IElement2D)parameterList[ 0 ];
                        IElement2D elementB = (IElement2D)parameterList[ 1 ];
                        Vector2 anchorA = (Vector2)parameterList[ 2 ];
                        Vector2 anchorB = (Vector2)parameterList[ 3 ];
                        JointFactory.CreateDistanceJoint( world, elements[ elementA ].Body, elements[ elementB ].Body, anchorA, anchorB );
                    }
                    break;
                case (int)XenFarseerFunc.AttachSliderJoint: // Params: [IElement2D] [IElement2D] [Vector2] [Vector2] [float] [float]
                    {
                        IElement2D elementA = (IElement2D)parameterList[ 0 ];
                        IElement2D elementB = (IElement2D)parameterList[ 1 ];
                        Vector2 anchorA = (Vector2)parameterList[ 2 ];
                        Vector2 anchorB = (Vector2)parameterList[ 3 ];
                        float minLength = (float)parameterList[ 4 ];
                        float maxLength = (float)parameterList[ 5 ];
                        JointFactory.CreateSliderJoint( world, elements[ elementA ].Body, elements[ elementB ].Body, anchorA, anchorB, minLength, maxLength );
                    }
                    break;
            }
        }

        private int numberOfParameters( int functionalityID )
        {
            switch( functionalityID )
            {
                case (int)XenFarseerFunc.SetScreenEdgeCollision: // Params: [bool]
                case (int)XenFarseerFunc.SetGravity: // Params: [Vector2]
                //case (int)XenFarseerFunc.AddElement: // Params: [IElement2D]
                //case (int)XenFarseerFunc.RemoveElement: // Params: [IElement2D]
                    return 1;
                //case (int)XenFarseerFunc.SetElementMass: // Params: [IElement2D] [float]
                //case (int)XenFarseerFunc.SetElementVelocity: // Params: [IElement2D] [Vector2]
                //case (int)XenFarseerFunc.SetElementAngularVelocity: // Params: [IElement2D] [float]
                //case (int)XenFarseerFunc.SetElementFriction: // Params: [IElement2D] [float]
                case (int)XenFarseerFunc.ApplyForce: // Params: [IElement2D] [Vector2]
                case (int)XenFarseerFunc.ApplyTorque: // Params: [IElement2D] [float]
                //case (int)XenFarseerFunc.AttachCollisionHandler: // Params: [IElement2D] [XenFarseerCollisionHandler]
                    return 2;
                case (int)XenFarseerFunc.AttachDistanceJoint: // Params: [IElement2D] [IElement2D] [Vector2] [Vector2]
                    return 4;
                case (int)XenFarseerFunc.AttachSliderJoint: // Params: [IElement2D] [IElement2D] [Vector2] [Vector2] [float] [float]
                    return 6;
                default:
                    throw new NotSupportedException( "Invalid functionality ID" );
            }
        }

        private bool OnCollision( Fixture fix1, Fixture fix2, Contact contact )
        {
            for( int i = 0; i < 4; i++ )
            {
                if( screenEdgeFixtures[ i ] == fix1 ||
                    screenEdgeFixtures[ i ] == fix2 )
                {
                    // It's colliding with one of the screen-edge fixtures
                    return isScreenEdgeCollidable;
                }
            }
            // TODO: Needs to handle collision rules
            IElement2D element1 = ( (IElement2D)fix1.UserData );
            IElement2D element2 = ( (IElement2D)fix2.UserData );
            if( elements.ContainsKey( element1 ) &&
                elements.ContainsKey( element2 ) )
            {
                gameLayer.OnCollision.Notify( new CollisionEventArgs( (ICollidableObject)element1, (ICollidableObject)element2 ) );
                return ( element1.IsCollidable && element2.IsCollidable );
            }

            return false;
        }

        #endregion
    }
}
