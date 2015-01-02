using XenGameBase;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Xen2D;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Media;

namespace PlatformerXen
{
    public class Game : GameBase
    {
        Physics _physics = new Physics();
        Action<CollisionEventArgs> _onCollision;

        public Game()
        {
            _onCollision = new Action<CollisionEventArgs>( OnElementCollision );
        }

        private void OnElementCollision( CollisionEventArgs collision )
        {
            if( collision.InvolvesTypes( typeof( Player ), typeof( Tile ) ) )
            {
                Tile tile = null;
                if( collision.ContainsParticipantOfType<Tile>( out tile ) )
                {
                    Player player = null;
                    collision.ContainsParticipantOfType<Player>( out player );

                    if( tile.CollisionClass == (uint)PlatformerCollisionClass.Tile_Platform )
                    {
                        _physics.ResolvePlayerPlatformCollision( player, tile );
                    }
                }
            }
            else if( collision.InvolvesTypes( typeof( Player ), typeof( Gem ) ) )
            {
                Gem gem = null;
                if( collision.ContainsParticipantOfType<Gem>( out gem ) )
                {
                    gem.Release();
                    SoundEffects.Get( SoundId.GemCollected ).Play();
                }
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //ShowDebug = true;
            MainScene.CollisionRules = new PlatformerCollisionRules();
            MainScene.OnCollision.Add( _onCollision );
            //var behavior = MouseScrollZoomBehavior.Acquire( MainScene, 1.05f );
            //MainScene.Behaviors.Add( behavior );

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play( Songs.Get( SongId.Music ) );

            Level.Load( 0, MainScene );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            _physics.Apply( MainScene.Children.Items, ref gameTime );
        }
    }
}
