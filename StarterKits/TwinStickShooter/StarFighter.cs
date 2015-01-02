using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace TwinStickShooter
{
    #region StarFighter Components

    public interface IStarFighterPart : IElement2D
    {
        float HitPoints { get; set; }
        bool IsDestroyed { get; set; }
    }

    public abstract class StarFighterPart<T> : ComplexElement2D<T>, IStarFighterPart
        where T : ComposableObject, new()
    {
        public float HitPoints { get; set; }
        public bool IsDestroyed { get; set; }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            IsDestroyed = false;
        }
    }

    public class PartFuselage : StarFighterPart<PartFuselage>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 25, 000 ),
            new Vector2( 50, 116 ),
            new Vector2( 25, 166 ),
            new Vector2( 00, 116 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Fuselage ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public PartFuselage()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }

    public class PartEnginePodLeft : StarFighterPart<PartEnginePodLeft>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 00, 38 ),
            new Vector2( 13, 00 ),
            new Vector2( 23, 43 ),
            new Vector2( 7, 114 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Engine_Pod_Left ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public PartEnginePodLeft()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }

    public class PartEnginePodRight : StarFighterPart<PartEnginePodRight>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 00, 42 ),
            new Vector2( 9, 00 ),
            new Vector2( 23, 38 ),
            new Vector2( 16, 114 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Engine_Pod_Right ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public PartEnginePodRight()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }

    public class PartWingLeft : StarFighterPart<PartWingLeft>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 72, 0 ),
            new Vector2( 60, 62 ),
            new Vector2( 0, 131 ),
            new Vector2( 34, 36 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Wing_Left ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public PartWingLeft()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            VisualComponents.RenderingExtent.ReAnchor( new Vector2( 72, 0 ) );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }

    public class PartWingRight : StarFighterPart<PartWingRight>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 0, 0 ),
            new Vector2( 37, 37 ),
            new Vector2( 73, 131 ),
            new Vector2( 12, 60 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Wing_Right ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public PartWingRight()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }

    public class Cockpit : StarFighterPart<Cockpit>
    {
        private static List<Vector2> _CollisionExtentVertices = new List<Vector2>()
        {
            new Vector2( 16, 0 ),
            new Vector2( 31, 54 ),
            new Vector2( 0, 54 ),
        };

        StaticSprite _texture = StaticSprite.Acquire( Textures.Get( TexId.Cockpit_Red ) );
        PolygonExtent _collisionExtent = PolygonExtent.Acquire();

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        public Cockpit()
        {
            _collisionExtent.Reset( _CollisionExtentVertices );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            VisualComponents.Add( _texture );
            VisualComponents.AddExtent( _collisionExtent );
            CollisionClass = (uint)CustomCollisionClass.Player;
        }
    }
    #endregion

    public interface IStarFighter : IElement2D
    {
        float TurnSpeed { get; }
        float Acceleration { get; }
        float Deceleration { get; }
        float CurrentSpeed { get; set; }
        float MaxSpeed { get; }
    }

    public class StarFighter : CompositeElement2D<StarFighter>, IStarFighter
    {
        PartFuselage _fuselage = PartFuselage.Acquire();
        PartEnginePodLeft _engineLeft = PartEnginePodLeft.Acquire();
        PartEnginePodRight _engineRight = PartEnginePodRight.Acquire();
        PartWingLeft _wingLeft = PartWingLeft.Acquire();
        PartWingRight _wingRight = PartWingRight.Acquire();

        StaticSprite _marker = StaticSprite.Acquire( Textures.Get( TexId.Marker_Red ), new Vector2( 3, 3 ) );

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );

            //_marker.RenderingExtent.Anchor = Vector2.Transform( RenderingExtent.ActualCenter, RenderingExtent.TranslateFrom * transformFromWorldToCamera );
            _marker.RenderingExtent.Anchor = Vector2.Transform( new Vector2( 100, 100 ), RenderingExtent.TranslateFrom * transformFromWorldToCamera );
            _marker.Draw( spriteBatch );

            _marker.RenderingExtent.Anchor += 100 * Vector2.UnitY;
            _marker.Draw( spriteBatch );
            //spriteBatch.DrawExtent( RenderingExtent, Color.Red );
            //spriteBatch.DrawExtent( RenderingExtent, Color.GreenYellow, transformFromWorldToCamera );
        }

        public float TurnSpeed { get; set; }
        public float Acceleration { get; set; }
        public float Deceleration { get; set; }
        public float CurrentSpeed { get; set; }
        public float MaxSpeed { get; set; }

        public StarFighter()
        {
            _fuselage.RenderingExtent.Anchor = new Vector2( 65, 0 );
            _wingRight.RenderingExtent.Anchor = new Vector2( 109, 81 );
            _wingLeft.RenderingExtent.Anchor = new Vector2( 72, 82 );
            _engineLeft.RenderingExtent.Anchor = new Vector2( 53, 46 );
            _engineRight.RenderingExtent.Anchor = new Vector2( 106, 46 );
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();

            Children.Add( _wingLeft );
            Children.Add( _wingRight );
            Children.Add( _engineLeft );
            Children.Add( _engineRight );
            Children.Add( _fuselage );
            VisualComponents.RenderingExtent.ReAnchor( new Vector2( 90, 100 ) );
            Behaviors.Add( StarFighterMoveBehavior.Acquire( this ) );
            Behaviors.Add( StarfighterCommandBehavior.Acquire( this ) );

            CollisionClass = (uint)CustomCollisionClass.Player;
            RenderingExtent.Scale = 0.5f * Vector2.One;

            TurnSpeed = MathHelper.PiOver2;
            Acceleration = 100;
            Deceleration = 100;
            CurrentSpeed = 0;
            MaxSpeed = 200;
        }

        public void ApplyHitFrom( Laser laser )
        {
            IStarFighterPart part = null;
            foreach( var child in Children.Items )
            {
                if( child.CollisionExtent.Intersects( laser.CollisionExtent ) )
                {
                    part = child as IStarFighterPart;
                    break;
                }
            }

            if( part != null )
            {
                part.DetachFromParent();
                Parent.Children.Add( part );

                part.IsDestroyed = true;
                part.Behaviors.ReleaseAndClearChildren();
                part.Behaviors.Add( LinearMoveBehavior.Acquire( part, History.LastVelocity ) );
                part.Behaviors.Add( RotationBehavior.Acquire( part, XenMath.GetRandomFloatBetween( -1.5f, 1.5f ) ) );
                part.Behaviors.Add( ExplodesAfterTimeBehavior.Acquire( part, Parent as ILayer, XenMath.GetRandomFloatBetween( 0.5f, 2.5f ) ) );
            }
        }

        public void SetWingAngle( float angle )
        {
            if( !_wingLeft.IsDestroyed )
                _wingLeft.RenderingExtent.Angle = angle;

            if( !_wingRight.IsDestroyed )
                _wingRight.RenderingExtent.Angle = -angle;
        }
    }
}
