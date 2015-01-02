using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingPooledObjectNList
    {
        class MockPooledObject : PooledObject<MockPooledObject>
        {
        }

        [TestMethod]
        public void ReleasingItemCausesItToAutomaticallyRemoveFromCollection()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();
            col.Add( element );
            
            Assert.AreEqual( 1, col.Count );

            element.Release();

            Assert.AreEqual( 0, col.Count );
        }

        [TestMethod]
        public void AddingElementToCollectionCausesReleasedEventCountToIncrement()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();

            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
            col.Add( element );

            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void InsertingElementToCollectionCausesReleasedEventCountToIncrement()
        {
            var col = new PooledObjectNList();

            col.Add( MockPooledObject.Acquire() );

            var element = MockPooledObject.Acquire();

            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
            col.Insert( 0, element );

            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ReleasingElementCausesNumSubscribersToBecomeZero()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();
            col.Add( element );

            Assert.AreNotEqual( 0, element.OnReleased.NumSubscribers );

            element.Release();

            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ClearingRemovesOnReleasedListenerFromElement()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();

            col.Add( element );
            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );
            col.Clear();
            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void RemovingElementCausesReleasedEventCountToDecrement()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();
            col.Add( element );
            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );

            col.Remove( element );
            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void SettingViaIndexerCausesNewElementReleasedEventCountToIncrement()
        {
            var col = new PooledObjectNList();
            var element = MockPooledObject.Acquire();

            col.Add( MockPooledObject.Acquire() );
            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
            col[ 0 ] = element;
            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );            
        }

        [TestMethod]
        public void SettingViaIndexerCausesOldElementReleasedEventToDetach()
        {
            var col = new PooledObjectNList();
            var oldElement = MockPooledObject.Acquire();
            var element = MockPooledObject.Acquire();

            col.Add( oldElement );
            Assert.AreEqual( 0, element.OnReleased.NumSubscribers );
            Assert.AreEqual( 1, oldElement.OnReleased.NumSubscribers );
            col[ 0 ] = element;
            Assert.AreEqual( 1, element.OnReleased.NumSubscribers );
            Assert.AreEqual( 0, oldElement.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ReleaseAndClearChildrenWorks()
        {
            var col = new PooledObjectNList();
            var child = MockPooledObject.Acquire();

            col.Add( child );
            Assert.AreEqual( PooledObjectState.Acquired, child.PoolState );
            Assert.AreEqual( 1, col.Items.Count );
            col.ReleaseAndClearChildren();
            Assert.AreEqual( 0, col.Items.Count );
            Assert.AreEqual( PooledObjectState.Available, child.PoolState );
        }
    }
}
