using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_GCTests
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

        public WhenUsingPooledObject()
        {

        }

        [TestMethod]
        public void InheritingPoolsGeneratesNoGarbage()
        {
            MockPoolType.Pool.InitPool( 1000 );
            MockPoolType[] poolArray = new MockPoolType[ 1000 ];

            HeapState before = HeapUtility.GetHeapStateBefore();
            for( int i = 0; i < 1000; i++ )
            {
                MockPoolType.Acquire();
            }
            HeapState after = HeapUtility.GetHeapStateAfter();
            Assert.AreEqual( before, after );
        }

        [TestMethod]
        public void InheritingPoolsGeneratesNoGarbageAlternativeSyntax1()
        {
            MockPoolType.Pool.InitPool( 1000 );
            MockPoolType[] poolArray = new MockPoolType[ 1000 ];

            HeapState hs = HeapTracker.Execute( () =>
            {
                for( int i = 0; i < 1000; i++ )
                {
                    MockPoolType.Acquire();
                }
            } );
            Assert.AreEqual( HeapState.Neutral, hs );
        }
    }
}
