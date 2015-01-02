using XenGameBase;
using Xen2D;

namespace PlatformerXen
{
    public class Background : Element2D<Background>
    {
        public static Background Acquire( TexId tex, int layerDepth )
        {
            var instance =_pool.Acquire();
            instance.Reset( tex, layerDepth );
            return instance;
        }

        StaticSprite _sprite;

        public void Reset( TexId tex, int layerDepth )
        {
            base.Reset();
            _sprite = StaticSprite.Acquire( Textures.Get( tex ) );
            DrawOrder = layerDepth;
            VisualComponent = _sprite;
        }

        protected override void ReleaseInternal()
        {
            base.ReleaseInternal();
            _sprite.Release();
            _sprite = null;
        }
    }
}
