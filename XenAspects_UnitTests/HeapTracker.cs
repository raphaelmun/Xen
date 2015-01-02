using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XenAspects_UnitTests
{
    public static class HeapTracker
    {
        private static HeapState _before, _after, _delta, _temp;

        public static HeapState Execute( Action action )
        {
            ResetBeforeHeapState();

            action.Invoke();

            _after = HeapUtility.GetHeapStateAfter();

            _delta.GCCount = _after.GCCount - _before.GCCount;
            _delta.TotalMemoryAllocated = _after.TotalMemoryAllocated - _before.TotalMemoryAllocated;
            return _delta;
        }

        public static void ResetBeforeHeapState()
        {
            _before = HeapUtility.GetHeapStateBefore();
        }

        //WARNING: Do not use actions with lexical closure as they will generate garbage.
        public static void AssertNeutralExecute( Action action )
        {
            _temp = Execute( action );
            Assert.AreEqual( HeapState.Neutral, _temp );
        }

        public static HeapState GetDeltaFromExecute()
        {
            return _delta;
        }
    }
}
