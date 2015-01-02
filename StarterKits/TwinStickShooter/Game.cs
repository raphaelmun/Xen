using XenGameBase;
using Microsoft.Xna.Framework;

namespace TwinStickShooter
{
    public partial class Game : GameBase
    {
        protected override void LoadContent()
        {
            BackgroundColor = Color.Black;

            MainScene.CollisionRules = new CollisionRules();

            var behavior = FireLaserOnMouseClickBehavior.Acquire();
            behavior.Reset( MainScene, 0 );
            MainScene.Behaviors.Add( behavior );

            MainScene.Behaviors.Add( MouseScrollZoomBehavior.Acquire( MainScene, 1.1f ) );

            MainScene.Children.Add( StarFighter.Acquire() );
        }
    }
}
