using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;

namespace Xen3D
{
    public class CachedModelDescriptor : ComposableObject<CachedModelDescriptor>, IModelInfo
    {
        public static CachedModelDescriptor Acquire( Model cachedModel, float pixelWidth, float pixelHeight, Matrix sourceTransform )
        {
            var instance = Pool.Acquire();
            instance.Reset( cachedModel, pixelWidth, pixelHeight, sourceTransform );
            return instance;
        }

        protected Matrix _sourceTransform;
        protected Model _asset;
        protected Vector2 _pixelSize;

        public Model Asset
        {
            get { return _asset; }
        }

        public virtual Matrix SourceTransform
        {
            get { return _sourceTransform; }
        }

        public virtual Vector2 PixelSize
        {
            get { return _pixelSize; }
        }

        public CachedModelDescriptor() { }

        public void Reset( Model cachedModel, float pixelWidth, float pixelHeight, Matrix sourceTransform )
        {
            _asset = cachedModel;
            _sourceTransform = sourceTransform;
            _pixelSize = new Vector2( pixelWidth, pixelHeight );
        }
    }
}
