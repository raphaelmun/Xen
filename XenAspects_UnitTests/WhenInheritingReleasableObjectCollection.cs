using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenInheritingPooledObjectNList
    {
        class MockPooledObject : PooledObject<MockPooledObject>
        {
        }

        class DerivedReleasableCollectionA : PooledObjectNList<MockPooledObject>
        {
            protected override bool BeforeAddItem( MockPooledObject item )
            {
                return false;
            }
        }

        [TestMethod]
        public void ReturningFalseForAddAbortsTheAddOperation()
        {
            var col = new DerivedReleasableCollectionA();
            var element = MockPooledObject.Acquire();

            Assert.AreEqual( 0, col.Count );
            col.Add( element );
            Assert.AreEqual( 0, col.Count );
            Assert.IsFalse( col.Contains( element ) );
        }

        [TestMethod]
        public void ReturningFalseForAddAbortsTheInsertOperation()
        {
            var col = new DerivedReleasableCollectionA();
            var element = MockPooledObject.Acquire();

            Assert.AreEqual( 0, col.Count );
            col.Insert( 0, element );
            Assert.AreEqual( 0, col.Count );
            Assert.IsFalse( col.Contains( element ) );
        }

        class DerivedReleasableCollectionB : PooledObjectNList<MockPooledObject>
        {
            public object OnAfterAddParameter = null;

            protected override void AfterAddItem( MockPooledObject item )
            {
                base.AfterAddItem( item );
                OnAfterAddParameter = item;
            }
        }

        [TestMethod]
        public void OnAfterAddIsCalledAfterAddWithExpectedParameter()
        {
            var col = new DerivedReleasableCollectionB();
            var element = MockPooledObject.Acquire();

            Assert.IsNull( col.OnAfterAddParameter );
            col.Add( element );
            Assert.IsNotNull( col.OnAfterAddParameter );
            Assert.AreEqual( element, col.OnAfterAddParameter );
        }

        [TestMethod]
        public void OnAfterAddIsCalledAfterInsertWithExpectedParameter()
        {
            var col = new DerivedReleasableCollectionB();
            var element = MockPooledObject.Acquire();

            Assert.IsNull( col.OnAfterAddParameter );
            col.Insert( 0, element );
            Assert.IsNotNull( col.OnAfterAddParameter );
            Assert.AreEqual( element, col.OnAfterAddParameter );
        }
    }
}