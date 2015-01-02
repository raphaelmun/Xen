using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenInheritingSortedPooledObjectNList
    {
        class MockObject : PooledObject<MockObject>
        {
            public int Value { get; set; }

            public MockObject() { }

            public MockObject( int value )
            {
                Value = value;
            }
        }

        class MockPooledObjectNList : SortedPooledObjectNList<MockObject>
        {
        }

        class MockSortComparerIncrementing : IComparer<MockObject>
        {
            public int Compare( MockObject x, MockObject y )
            {
                return x.Value - y.Value;
            }
        }

        class MockSortComparerDecrementing : IComparer<MockObject>
        {
            public int Compare( MockObject x, MockObject y )
            {
                return y.Value - x.Value;
            }
        }

        [TestMethod]
        public void DefaultInstanceHasNullSortComparer()
        {
            var collection = new MockPooledObjectNList();
            Assert.IsNull( collection.SortComparer );
        }

        [TestMethod]
        public void AddingNewObjectsToDefaultInstanceYieldsAddedOrder()
        {
            var obj0 = new MockObject( 0 );
            var obj1 = new MockObject( 1 );
            var obj2 = new MockObject( 2 );
            var collection = new MockPooledObjectNList();
            collection.Add( obj1 );
            collection.Add( obj0 );
            collection.Add( obj2 );

            Assert.AreEqual( obj1, collection[ 0 ] );
            Assert.AreEqual( obj0, collection[ 1 ] );
            Assert.AreEqual( obj2, collection[ 2 ] );
        }

        [TestMethod]
        public void SettingSortComparerOnCollectionCausesAddedItemsToBeSorted()
        {
            var obj0 = new MockObject( 0 );
            var obj1 = new MockObject( 1 );
            var obj2 = new MockObject( 2 );
            var collection = new MockPooledObjectNList() { SortComparer = new MockSortComparerIncrementing() };

            collection.Add( obj1 );
            collection.Add( obj0 );
            collection.Add( obj2 );

            Assert.AreEqual( obj0, collection[ 0 ] );
            Assert.AreEqual( obj1, collection[ 1 ] );
            Assert.AreEqual( obj2, collection[ 2 ] );
        }

        [TestMethod]
        public void SettingSortComparerOnNonEmptyCollectionCausesItemsToBeSortedInNewSortOrder()
        {
            var obj0 = new MockObject( 0 );
            var obj1 = new MockObject( 1 );
            var obj2 = new MockObject( 2 );
            var collection = new MockPooledObjectNList();

            collection.Add( obj1 );
            collection.Add( obj0 );
            collection.Add( obj2 );

            collection.SortComparer = new MockSortComparerDecrementing();

            Assert.AreEqual( obj2, collection[ 0 ] );
            Assert.AreEqual( obj1, collection[ 1 ] );
            Assert.AreEqual( obj0, collection[ 2 ] );
        }
    }
}
