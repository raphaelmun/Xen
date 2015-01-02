using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingNListTemplate
    {
        [TestMethod]
        public void OnItemAddedGetsCalledWhenItemIsAdded()
        {
            NListTemplate<object> list = new NListTemplate<object>();

            bool onItemBeforeAddedCalled = false;
            list.OnItemBeforeAdded.Add( obj =>
                {
                    onItemBeforeAddedCalled = true;
                    Assert.AreEqual( 0, list.Items.Count );
                } );

            bool onItemAfterAddedCalled = false;
            list.OnItemAfterAdded.Add( obj => 
                {
                    onItemAfterAddedCalled = true;
                    Assert.AreEqual( 1, list.Items.Count );
                } );

            Assert.IsFalse( onItemAfterAddedCalled );
            Assert.IsFalse( onItemBeforeAddedCalled );

            list.Add( new object() );

            Assert.IsTrue( onItemBeforeAddedCalled );
            Assert.IsTrue( onItemAfterAddedCalled );
        }

        [TestMethod]
        public void OnItemAfterAddedGetsCalledOnEndEnumeration()
        {
            NListTemplate<object> list = new NListTemplate<object>();

            bool onItemAfterAddedCalled = false;
            list.OnItemAfterAdded.Add( obj => onItemAfterAddedCalled = true );

            Assert.IsFalse( onItemAfterAddedCalled );

            list.BeginEnumeration();
            list.Add( new object() );

            Assert.IsFalse( onItemAfterAddedCalled );

            list.EndEnumeration();

            Assert.IsTrue( onItemAfterAddedCalled );
        }

        [TestMethod]
        public void OnItemAfterAddedGetsCalledWhenItemIsInserted()
        {
            NListTemplate<object> list = new NListTemplate<object>();

            bool onItemAddedCalled = false;
            list.OnItemAfterAdded.Add( obj => onItemAddedCalled = true );

            Assert.IsFalse( onItemAddedCalled );

            list.Insert( 0, new object() );

            Assert.IsTrue( onItemAddedCalled );
        }

        [TestMethod]
        public void OnItemRemovedGetsCalledWhenItemIsRemoved()
        {
            NListTemplate<object> list = new NListTemplate<object>();
            object obj = new object();
            list.Add( obj );

            bool onItemBeforeRemovedCalled = false;
            list.OnItemBeforeRemoved.Add( o =>
            {
                onItemBeforeRemovedCalled = true;
                Assert.AreEqual( 1, list.Count );
            } );

            bool onItemAfterRemovedCalled = false;
            list.OnItemAfterRemoved.Add( o =>
            {
                onItemAfterRemovedCalled = true;
                Assert.AreEqual( 0, list.Count );
            } );

            Assert.IsFalse( onItemBeforeRemovedCalled );
            Assert.IsFalse( onItemAfterRemovedCalled );

            list.Remove( obj );

            Assert.IsTrue( onItemBeforeRemovedCalled );
            Assert.IsTrue( onItemAfterRemovedCalled );
        }

        [TestMethod]
        public void OnItemRemovedGetsCalledWhenItemIsRemovedDuringEnumeration()
        {
            NListTemplate<object> list = new NListTemplate<object>();
            object obj = new object();
            list.Add( obj );

            bool onItemRemovedCalled = false;
            list.OnItemAfterRemoved.Add( o => onItemRemovedCalled = true );

            Assert.IsFalse( onItemRemovedCalled );

            list.BeginEnumeration();
            list.Remove( obj );

            Assert.IsFalse( onItemRemovedCalled );

            list.EndEnumeration();

            Assert.IsTrue( onItemRemovedCalled );
        }

        [TestMethod]
        public void SetUsingIndexerCallsAddedAndRemoved()
        {
            NListTemplate<object> list = new NListTemplate<object>();
            list.Add( new object() );

            bool onItemAfterAddedCalled = false;
            list.OnItemAfterAdded.Add( o => onItemAfterAddedCalled = true );

            bool onItemAfterRemovedCalled = false;
            list.OnItemAfterRemoved.Add( o => onItemAfterRemovedCalled = true );

            Assert.IsFalse( onItemAfterAddedCalled );
            Assert.IsFalse( onItemAfterRemovedCalled );

            list[ 0 ] = new object();

            Assert.IsTrue( onItemAfterAddedCalled );
            Assert.IsTrue( onItemAfterRemovedCalled );
        }

        [TestMethod]
        public void ClearingItemsCallsBeforeAndAfterItemRemoved()
        {
            NListTemplate<object> list = new NListTemplate<object>();
            list.Add( new object() );
            list.Add( new object() );

            int onItemBeforeRemovedCalled = 0;
            int onItemAfterRemovedCalled = 0;
            list.OnItemBeforeRemoved.Add( o =>
                {
                    onItemBeforeRemovedCalled++;
                } );

            list.OnItemAfterRemoved.Add( o =>
                {
                    onItemAfterRemovedCalled++;
                } );


            Assert.AreEqual( 0, onItemBeforeRemovedCalled );
            Assert.AreEqual( 0, onItemAfterRemovedCalled );
            Assert.AreEqual( 2, list.Count );

            list.Clear();

            Assert.AreEqual( 0, list.Count );
            Assert.AreEqual( 2, onItemBeforeRemovedCalled );
            Assert.AreEqual( 2, onItemAfterRemovedCalled );
        }
    }
}
