using System.Collections.Generic;
using Xen2D;

namespace XenGameBase
{
    public class SortedElementCollection : SortedRenderable2DCollection<IElement2D>
    {
        public SortedElementCollection()
        {
            SortComparer = IncrementingElement2DComparer.Instance;
        }
    }
}
