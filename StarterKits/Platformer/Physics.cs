using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XenGameBase;

namespace PlatformerXen
{
    public class Physics
    {
        public static float DefaultGravity = 9.8f;
        public static float PixelsPerMeter = 10;
        public static float Gravity { get; set; }

        public Physics()
        {
            Gravity = DefaultGravity;
        }

        public void Apply( List<IElement2D> entities, ref GameTime gameTime )
        {
            foreach( var entity in entities )
            {
                if( entity is Player )
                {
                    Player player = entity as Player;

                    //Apply gravity to the player
                    float verticalVelocityDueToGravity = ( player.SupportingPlatforms.Count == 0 ) ? 
                        ( (float)gameTime.ElapsedGameTime.TotalSeconds * Gravity * 20 ) : 0;

                    player.Velocity += Vector2.UnitY * verticalVelocityDueToGravity;

                    if( ( ( player.RightAdjacentPlatform != null ) && ( player.Velocity.X > 0 ) ) ||
                        ( ( player.LeftAdjacentPlatform != null ) && ( player.Velocity.X < 0 ) ) )
                    {
                        player.Velocity *= Vector2.UnitY;
                    }

                    player.RenderingExtent.Anchor += ( player.Velocity / PixelsPerMeter );
                }
            }
        }

        /// <summary>
        /// Resolves a collision between the player and the specified platform tile by moving the player back toward the direction
        /// they came from.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="platform">The platform tile that the player intersects</param>
        public void ResolvePlayerPlatformCollision( Player player, Tile platform )
        {
            if( player.Intersects( platform ) )
            {
                Vector2 lastDisplacement = player.History.Last.Position - player.History.SecondToLast.Position;

                if( lastDisplacement == Vector2.Zero )
                    return;

                if( player.SupportingPlatforms.Contains( platform ) || platform == player.LeftAdjacentPlatform || platform == player.RightAdjacentPlatform )
                    return;

                float displacementIntoPlatformX = 0, displacementIntoPlatformY = 0;
                float displacementNeededToResolveX, displacementNeededToResolveY;

                if( lastDisplacement.Y > 0 )
                    //player was moving down
                    displacementIntoPlatformY = player.CollisionExtent.HighestY - platform.CollisionExtent.LowestY;
                else if( lastDisplacement.Y < 0 )
                    //player was moving up
                    displacementIntoPlatformY = player.CollisionExtent.LowestY - platform.CollisionExtent.HighestY;

                if( lastDisplacement.X > 0 )
                    //player was moving right
                    displacementIntoPlatformX = player.CollisionExtent.HighestX - platform.CollisionExtent.LowestX;
                else if( lastDisplacement.X < 0 )
                    //player was moving left
                    displacementIntoPlatformX = player.CollisionExtent.LowestX - platform.CollisionExtent.HighestX;

                displacementNeededToResolveY = ( displacementIntoPlatformY / lastDisplacement.Y );
                displacementNeededToResolveX = ( displacementIntoPlatformX / lastDisplacement.X );

                float displacementToUse;
                if( lastDisplacement.X == 0 )
                    displacementToUse = displacementNeededToResolveY;
                else if( lastDisplacement.Y == 0 )
                    displacementToUse = displacementNeededToResolveX;
                else
                    displacementToUse = Math.Min( displacementNeededToResolveY, displacementNeededToResolveX );

                if( ( player.SupportingPlatforms.Count > 0 ) && 
                    ( player.SupportingPlatforms[ 0 ].CollisionExtent.LowestY == platform.CollisionExtent.LowestY ) )
                {
                    player.SupportingPlatforms.Add( platform );
                    displacementToUse = 0;
                }

                if( displacementToUse == 0 )
                    return;

                player.RenderingExtent.Anchor -= displacementToUse * lastDisplacement;

                var lastHistory = player.History.Last;
                lastHistory.Position -= displacementToUse * lastDisplacement;
                player.History.Last = lastHistory;


                if( player.CollisionExtent.HighestX <= platform.CollisionExtent.LowestX )
                {
                    player.Velocity = new Vector2( 0, player.Velocity.Y );
                    player.RightAdjacentPlatform = platform;
                }
                else if( player.CollisionExtent.LowestX >= platform.CollisionExtent.HighestX )
                {
                    player.Velocity = new Vector2( 0, player.Velocity.Y );
                    player.LeftAdjacentPlatform = platform;
                }

                if( ( player.CollisionExtent.HighestY <= platform.CollisionExtent.LowestY ) ||
                    ( player.CollisionExtent.LowestY > platform.CollisionExtent.HighestY ) )
                {
                    player.Velocity = new Vector2( player.Velocity.X, 0 );
                    player.SupportingPlatforms.Add( platform );
                }
                else if( player.CollisionExtent.LowestY == platform.CollisionExtent.HighestY )
                {
                    player.Velocity = new Vector2( player.Velocity.X, 0 );
                }
            }
        }
    }
}
