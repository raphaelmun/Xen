using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingPooledObject
    {
        const int largeArraySize = short.MaxValue;
        public class MockPoolType : PooledObject<MockPoolType>
        {
            int[] largeArray = new int[ largeArraySize ];
            public MockPoolType()
            {
            }
        }

        [TestMethod]
        public void ReleasingPooledObjectChangesPoolState()
        {
            MockPoolType.Pool.InitPool( 20 );
            MockPoolType obj = MockPoolType.Acquire();
            Assert.IsTrue( obj.PoolState == PooledObjectState.Acquired ); //Is this object being used?  
            obj.Release();
            Assert.IsTrue( obj.PoolState == PooledObjectState.Available );
        }

        [TestMethod]
        public void ReleasingPoolsGeneratesNoGarbage()
        {
            MockPoolType.Pool.InitPool( 1000 );
            MockPoolType[] poolArray = new MockPoolType[ 1000 ];
            HeapUtility.CollectGarbage();
            for( int i = 0; i < 1000; i++ )
            {
                poolArray[ i ] = MockPoolType.Acquire();
            }
            long totalMemoryBeforeRelease = HeapUtility.GetTotalAllocatedMemory();
            int totalCollectionCountBeforeRelease = HeapUtility.GetTotalCollectionCount();
            for( int i = 0; i < 1000; i++ )
            {
                poolArray[ i ].Release();
            }
            int totalCollectionCountAfterRelease = HeapUtility.GetTotalCollectionCount();
            long totalMemoryAfterRelease = HeapUtility.GetTotalAllocatedMemory();
            Assert.AreEqual( totalCollectionCountBeforeRelease, totalCollectionCountAfterRelease );
        }
    }
}
