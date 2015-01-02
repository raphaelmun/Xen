using System.Collections.Generic;
using Xen2D;

namespace XenGameBase
{
    public class SortedLayerCollection : SortedRenderable2DCollection<ILayer>
    {
        public SortedLayerCollection()
        {
            SortComparer = IncrementingLayerComparer.Instance;
        }
    }
}
