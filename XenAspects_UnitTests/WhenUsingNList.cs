using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingNList
    {
        [TestMethod]
        public void AddWorks()
        {
            NList<object> list = new NList<object>();
            object obj = new object();

            Assert.AreEqual( 0, list.Count );
            list.Add( obj );

            Assert.AreEqual( 1, list.Count );
            Assert.IsTrue( list.Contains( obj ) );
        }

        [TestMethod]
        public void AddNullReturnsfalse()
        {
            NList<object> list = new NList<object>();

            Assert.IsFalse( list.Add( null ) );
            Assert.AreEqual( 0, list.Count );
        }

        [TestMethod]
        public void AddDuplicateReturnsFalse()
        {
            NList<object> list = new NList<object>();
            object obj = new object();

            list.Add( obj );
            Assert.IsTrue( list.Contains( obj ) );
            Assert.IsFalse( list.Add( obj ) );
            Assert.AreEqual( 1, list.Count );
        }

        [TestMethod]
        public void RemoveWorks()
        {
            NList<object> list = new NList<object>();
            object obj = new object();

            Assert.AreEqual( 0, list.Count );
            Assert.IsFalse( list.Contains( obj ) );

            list.Add( obj );
            list.Remove( obj );

            Assert.AreEqual( 0, list.Count );
            Assert.IsFalse( list.Contains( obj ) );
        }

        [TestMethod]
        public void AddingElementsDuringForeachWorks()
        {
            NList<object> list = new NList<object>();
            object obj1 = new object();
            object obj2 = new object();
            list.Add( obj1 );

            list.BeginEnumeration();
            foreach( object obj in list.Items )
            {
                list.Add( obj2 );
            }
            list.EndEnumeration();

            Assert.IsTrue( list.Contains( obj1 ) );
            Assert.IsTrue( list.Contains( obj2 ) );
        }

        [TestMethod]
        public void RemovingElementsDuringForeachWorks()
        {
            NList<object> list = new NList<object>();
            object obj1 = new object();
            object obj2 = new object();
            list.Add( obj1 );
            list.Add( obj2 );

            list.BeginEnumeration();
            foreach( object obj in list.Items )
            {
                list.Remove( obj2 );
            }
            list.EndEnumeration();

            Assert.IsTrue( list.Contains( obj1 ) );
            Assert.IsFalse( list.Contains( obj2 ) );
        }

        [TestMethod]
        public void AddingItemWhileIteratingAddsItAfterEndEnumeration()
        {
            NList<object> list = new NList<object>();

            int numObjectsToAdd = 5;
            list.BeginEnumeration();
            for( int i = 0; i < numObjectsToAdd; i++ )
            {
                list.Add( new object() );
            }

            Assert.AreEqual( 0, list.Count );
            list.EndEnumeration();

            Assert.AreEqual( numObjectsToAdd, list.Count );
        }

        [TestMethod]
        public void ClearingItemsWorks()
        {
            NList<object> list = new NList<object>();

            Assert.AreEqual( 0, list.Count );

            list.Add( new object() );
            list.Add( new object() );
            list.Add( new object() );

            Assert.AreEqual( 3, list.Count );
            Assert.AreEqual( 3, list.Items.Count );
            list.Clear();

            Assert.AreEqual( 0, list.Count );
            Assert.AreEqual( 0, list.Items.Count );
        }

        [TestMethod]
        public void InsertRemoveAtRoundTripWorks()
        {
            var obj = new object();

            NList<object> list = new NList<object>();

            list.Add( new object() );
            list.Add( new object() );

            Assert.AreEqual( 2, list.Count );
            Assert.AreNotEqual( obj, list[ 1 ] );

            list.Insert( 1, obj );

            Assert.AreEqual( 3, list.Count );
            Assert.AreEqual( obj, list[ 1 ] );

            list.RemoveAt( 1 );

            Assert.AreEqual( 2, list.Count );
            Assert.AreNotEqual( obj, list[ 1 ] );
        }

        [TestMethod]
        public void RemoveAtDuringEnumerationThrowsException()
        {
            NList<object> list = new NList<object>();

            list.Add( new object() );
            list.Add( new object() );

            list.BeginEnumeration();

            try
            {
                list.RemoveAt( 0 );
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void InsertDuringEnumerationThrowsException()
        {
            NList<object> list = new NList<object>();
            list.BeginEnumeration();

            try
            {
                list.Insert( 0, new object() );
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void IndexerSettingWorks()
        {
            var obj = new object();
            NList<object> list = new NList<object>();
            list.Add( new object() );
            list[ 0 ] = obj;
            Assert.AreEqual( 0, list.IndexOf( obj ) );
        }

        [TestMethod]
        public void UsingIndexerSettingDuringEnumerationThrowsException()
        {
            NList<object> list = new NList<object>();
            
            list.BeginEnumeration();

            try
            {
                list[ 0 ] = new object();
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void AddingMoreItemsThanDefaultBufferCapacityWorks()
        {
            NList<object> list = new NList<object>();

            list.BeginEnumeration();

            int numberThatExceedsCapacity = NList<object>.DefaultCapacity + 5;
            for( int i = 0; i < numberThatExceedsCapacity ; i++ )
            {
                list.Add( new object() );
            }

            Assert.AreEqual( 0, list.Items.Count );

            list.EndEnumeration();

            Assert.AreEqual( numberThatExceedsCapacity, list.Items.Count );
        }

        [TestMethod]
        public void BufferedOperationsResolveInExpectedOrder()
        {
            NList<object> list = new NList<object>();

            list.BeginEnumeration();

            list.Add( new object() );
            list.Add( new object() );
            list.Add( new object() );

            var obj1 = new object();
            var obj2 = new object();

            list.Clear();
            list.Add( obj1 );
            list.Add( obj2 );

            list.EndEnumeration();
            Assert.AreEqual( 2, list.Items.Count );
            Assert.AreEqual( obj1, list.Items[ 0 ] );
            Assert.AreEqual( obj2, list.Items[ 1 ] );
        }

        [TestMethod]
        public void BufferedOperationsAreNotResolvedUntilEndEnumerationCallsEqualsBeginEnumerationCAlls()
        {
            NList<object> list = new NList<object>();

            //Calling BeginEnumeration indicates that we are performing a nested enumeration
            list.BeginEnumeration();
            list.BeginEnumeration();

            list.Add( new object() );
            list.Add( new object() );
            list.Add( new object() );

            list.EndEnumeration();
            Assert.AreEqual( 0, list.Items.Count );

            list.EndEnumeration();
            Assert.AreEqual( 3, list.Items.Count );
        }
    }
}
