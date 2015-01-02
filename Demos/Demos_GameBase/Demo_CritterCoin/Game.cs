using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenGameBase;

namespace Demo_CritterCoin
{
    #region Textures
    enum TexId : int
    {
        [ContentIdentifier( "textures\\critter_180x180" )]
        Critter,
        [ContentIdentifier( "textures\\coin_64x64" )]
        Coin,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    class Textures : Texture2DCache<TexId>
    {
        public static readonly Textures Instance = new Textures();

        public static Texture2D Get( TexId tex )
        {
            return Instance[ tex ];
        }

        public override Texture2D this[ TexId tex ]
        {
            get { return this[ (int)tex ]; }
        }
    }
    #endregion

    #region Sound Effects
    enum SfxId : int
    {
        [ContentIdentifier( "sfx\\coin_pickup" )]
        CoinPickup,
    }

    /// <summary>
    /// This declaration of Textures serves as a way to avoid casting the TexId enum to int all over the code.  
    /// It also defines a singleton for accessing the textures.  
    /// </summary>
    class SoundEffects : SoundEffectCache<SfxId>
    {
        public static readonly SoundEffects Instance = new SoundEffects();

        public static SoundEffect Get( SfxId sfx )
        {
            return Instance[ sfx ];
        }

        public override SoundEffect this[ SfxId sfx ]
        {
            get { return this[ (int)sfx ]; }
        }
    }
    #endregion

    enum CustomCollisionClasses : uint
    {
        Player = 1,
        Coin = 2,
    }

    /// <summary>
    /// This demo shows how to listen on the collision event for a layer and handle collisions
    /// </summary>
    public class GameMain : GameBase
    {
        protected override void LoadContent()
        {
            CollisionLifetimeEntry.Pool.InitPool( 120 );

            Textures.Get( TexId.Coin );
            SoundEffects.Get( SfxId.CoinPickup );

            var player = ComplexPlayer.Acquire();

            var collisionRules = new CollisionRuleSet();
            collisionRules.AddRule( (uint)CustomCollisionClasses.Coin, (uint)CustomCollisionClasses.Player, 0.1f );
            MainScene.CollisionRules = collisionRules;

            MainScene.Children.Add( player );

            for( int i = 0; i < 100; i++ )
            {
                Vector2 randomPos = new Vector2( XenMath.GetRandomFloatBetween( 100, 700 ), XenMath.GetRandomFloatBetween( 100, 380 ) );
                var coin = ComplexCoin.Acquire();
                coin.RenderingExtent.Anchor = randomPos;
                coin.DrawOrder = 5;
                MainScene.Children.Add( coin );
            }

            MainScene.Opacity = 0.5f;
            base.LoadContent();
        }

        protected override void OnMainSceneCollisionHandler( CollisionEventArgs collisionEvent )
        {
            ComplexCoin coin = null;
            if( collisionEvent.ContainsParticipantOfType<ComplexCoin>( out coin ) )
                coin.Release();

            SoundEffects.Instance[ SfxId.CoinPickup ].Play( 0.1f, 0, 0 );
        }
    }
}
