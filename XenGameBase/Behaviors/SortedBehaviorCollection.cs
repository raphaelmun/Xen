using System.Collections.Generic;
using Xen2D;

namespace XenGameBase
{
    public class SortedBehaviorCollection : SortedRenderable2DCollection<IBehavior>
    {
        public SortedBehaviorCollection()
        {
            SortComparer = IncrementingBehaviorComparer.Instance;
        }
    }
}
