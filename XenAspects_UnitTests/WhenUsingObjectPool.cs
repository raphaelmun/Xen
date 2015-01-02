using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingObjectPool
    {
        internal class TestObject : PooledObject<TestObject>
        {
        }

        [TestMethod]
        public void DefaultPoolIsNotLocked()
        {
            Assert.IsFalse( TestObject.Pool.Locked );
        }

        [TestMethod]
        public void UnlockedPoolCanExpandOnAcquire()
        {
            TestObject.Pool.InitPool( 1 );
            TestObject to1 = TestObject.Acquire();
            TestObject to2 = TestObject.Acquire();
        }

        [TestMethod]
        public void LockedPoolThrowsWhenAcquiringOverCapacity()
        {
            TestObject.Pool.InitPool( 1 );
            TestObject.Pool.Locked = true;
            TestObject to1 = TestObject.Acquire();
            try
            {
                TestObject to2 = TestObject.Acquire();
                Assert.Fail( "should not reach here" );
            }
            catch( ObjectPoolException )
            {
            }
        }

        [TestMethod]
        public void PooledObjectIsAtCorrectStateDuringAcquireReleaseCycle()
        {
            TestObject.Pool.InitPool( 1 );
            TestObject to1 = TestObject.Acquire();

            Assert.AreEqual( PooledObjectState.Acquired, to1.PoolState );
            to1.Release();
            Assert.AreEqual( PooledObjectState.Available, to1.PoolState );
        }

        [TestMethod]
        public void ReleaseAcquireCycleReturnsSameObjectForSinglePool()
        {
            TestObject.Pool.InitPool( 1 );
            TestObject to1 = TestObject.Acquire();

            to1.Release();
            TestObject.Pool.CleanUp();

            TestObject to2 = TestObject.Acquire();
            Assert.AreEqual( to1, to2 );
        }

        [TestMethod]
        public void PoolCapacityReturnsCorrectResult()
        {
            TestObject.Pool.InitPool( 2 );
            Assert.AreEqual( 2, TestObject.Pool.Capacity );
        }

        [TestMethod]
        public void PoolAvailableInstancesReturnsCorrectResult()
        {
            TestObject.Pool.InitPool( 2 );

            Assert.AreEqual( 2, TestObject.Pool.NumAvailableInstances );
            TestObject o1 = TestObject.Acquire();
            Assert.AreEqual( 1, TestObject.Pool.NumAvailableInstances );
            TestObject o2 = TestObject.Acquire();
            Assert.AreEqual( 0, TestObject.Pool.NumAvailableInstances );

            o1.Release();
            Assert.AreEqual( 1, TestObject.Pool.NumAvailableInstances );
            o2.Release();
            Assert.AreEqual( 2, TestObject.Pool.NumAvailableInstances );
        }
    }
}
