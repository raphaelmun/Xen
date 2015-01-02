using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_GCTests
{
    [TestClass]
    public class WhenUsingNList
    {
        [TestMethod]
        public void GenericListIterationDoesNotGenerateGarbage()
        {
            List<object> list = new List<object>();
            for( int i = 0; i < 1000; i++ )
            {
                list.Add( new object() );
            }

            HeapUtility.GetHeapStateBefore();

            foreach( object obj in list )
            {
                obj.GetHashCode();
            }

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void IterationDoesNotGenerateGarbage()
        {
            NList<object> list = new NList<object>();
            for( int i = 0; i < 1000; i++ )
            {
                list.Add( new object() );
            }

            HeapUtility.GetHeapStateBefore();

            foreach( object obj in list.Items )
            {
                obj.GetHashCode();
            }

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void ContainsDoesNotGenerateGarbage()
        {
            NList<object> list = new NList<object>();
            object obj = new object();
            list.Add( obj );

            Assert.IsTrue( list.Contains( obj ) );

            HeapUtility.GetHeapStateBefore();

            list.Contains( obj );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void RemoveDoesNotGenerateGarbage()
        {
            NList<object> list = new NList<object>();
            object obj = new object();
            list.Add( obj );

            HeapUtility.GetHeapStateBefore();

            list.Remove( obj );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
