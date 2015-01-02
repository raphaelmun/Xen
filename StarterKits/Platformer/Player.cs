using XenGameBase;
using Xen2D;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using XenAspects;

namespace PlatformerXen
{
    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Dying,
        Celebrating
    }

    public class Player : ComplexElement2D<Player>
    {
        AnimatedSprite _playerRunning = AnimatedSprite.Acquire();
        AnimatedSprite _playerJumping = AnimatedSprite.Acquire();
        ISprite _playerIdle = StaticSprite.Acquire( Textures.Get( TexId.Player_Idle ) );
        IRectangularExtent _collisionExtent = RectangularExtent.Acquire( 20, 8, 28, 56 );
        IBehavior _moveBehavior;

        public override IExtent CollisionExtent { get { return _collisionExtent; } }

        /// <summary>
        /// Player's current velocity in meters per second
        /// </summary>
        public Vector2 Velocity { get; set; }

        private PlayerState _state = PlayerState.Idle;
        public PlayerState State { 
            get 
            { 
                return _state; 
            }
            set
            {
                _state = value;
                _playerRunning.Visible = false;
                _playerIdle.Visible = false;
                _playerJumping.Visible = false;
                switch( _state )
                {
                    case PlayerState.Running:
                        _playerRunning.Visible = true;
                        break;
                    case PlayerState.Idle:
                        _playerIdle.Visible = true;
                        break;
                    case PlayerState.Jumping:
                        _playerJumping.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private PooledObjectNList<Tile> _currentTiles = new PooledObjectNList<Tile>();
        public PooledObjectNList<Tile> SupportingPlatforms { get { return _currentTiles; } }
        public Tile RightAdjacentPlatform { get; set; }
        public Tile LeftAdjacentPlatform { get; set; }
        
        public Player()
        {
            _moveBehavior = PlayerMoveBehavior.Acquire( this, 60.0f );
            State = PlayerState.Jumping;
        }

        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            CollisionClass = (uint)PlatformerCollisionClass.Player;
            _playerRunning.ResetFromTopLeft( Textures.Get( TexId.Player_Run ), 1, 10, Vector2.Zero );
            _playerJumping.ResetFromTopLeft( Textures.Get( TexId.Player_Jump ), 1, 11, Vector2.Zero );
            _playerJumping.FramesPerSecond = 10;
            VisualComponents.Add( _playerIdle );
            VisualComponents.Add( _playerRunning );
            VisualComponents.Add( _playerJumping );
            VisualComponents.AddExtent( _collisionExtent );
            Behaviors.Add( _moveBehavior );
        }

        protected override void UpdateInternal( GameTime gameTime )
        {
            base.UpdateInternal( gameTime );

            SupportingPlatforms.BeginEnumeration();
            foreach( var tile in SupportingPlatforms.Items )
            {
                if( !tile.CollisionExtent.Intersects( CollisionExtent ) )
                {
                    SupportingPlatforms.Remove( tile );
                }
            }
            SupportingPlatforms.EndEnumeration();

            if( ( null != RightAdjacentPlatform ) && ( !RightAdjacentPlatform.CollisionExtent.Intersects( CollisionExtent ) ) )
            {
                RightAdjacentPlatform = null;
            }

            if( ( null != LeftAdjacentPlatform ) && ( !LeftAdjacentPlatform.CollisionExtent.Intersects( CollisionExtent ) ) )
            {
                LeftAdjacentPlatform = null;
            }

            if( Velocity == Vector2.Zero )
            {
                State = PlayerState.Idle;
            }
            else
            {
                if( Velocity.X > 0 )
                {
                    //player moving right
                    _playerJumping.SpriteEffects = SpriteEffects.FlipHorizontally;
                    _playerRunning.SpriteEffects = SpriteEffects.FlipHorizontally;
                }
                else if( Velocity.X < 0 )
                {
                    //player moving left
                    _playerJumping.SpriteEffects = SpriteEffects.None;
                    _playerRunning.SpriteEffects = SpriteEffects.None;
                }

                if( SupportingPlatforms.Count == 0 )
                {
                    State = PlayerState.Jumping;
                }
                else
                {
                    State = PlayerState.Running;
                }
            }
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            foreach( var tile in SupportingPlatforms.Items )
            {
                spriteBatch.DrawExtent( tile.CollisionExtent, Color.Orange, transformFromWorldToCamera );
            }
        }
    }
}
