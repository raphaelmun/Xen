using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenBoxingValueTypes
    {
        struct TestStruct
        {
            public float F;
            public int I;
        }

        [TestMethod]
        public void AddingStructsToGenericCollectionCausesBoxing()
        {
            List<TestStruct> structs = new List<TestStruct>();

            long memBefore = HeapUtility.GetTotalAllocatedMemory();
            for( int i = 0; i < 1000; i++ )
            {
                //Each struct here gets boxed, which generates garbage
                structs.Add( new TestStruct() );
            }
            long memAfter = HeapUtility.GetTotalAllocatedMemory() - memBefore;
            Assert.AreNotEqual( 0, memAfter );
        }

        [TestMethod]
        public void AddingStructsToPresetSizeGenericCollectionDoesNotCauseHeapAllocation()
        {
            List<TestStruct> structs = new List<TestStruct>( 1000 );

            long memBefore = HeapUtility.GetTotalAllocatedMemory();
            for( int i = 0; i < 1000; i++ )
            {
                //Each struct here gets boxed, which generates garbage
                structs.Add( new TestStruct() );
            }
            long memAfter = HeapUtility.GetTotalAllocatedMemory() - memBefore;
            Assert.AreEqual( 0, memAfter );
        }

        [TestMethod]
        public void AddingStructsToArrayTypeDoesNotCauseBoxing()
        {
            TestStruct[] structArray = new TestStruct[ 1000 ];

            long memBefore = HeapUtility.GetTotalAllocatedMemory();
            for( int i = 0; i < 1000; i++ )
            {
                structArray[ i ] = new TestStruct();
            }
            long memAfter = HeapUtility.GetTotalAllocatedMemory() - memBefore;
            Assert.AreEqual( 0, memAfter );
        }
    }
}
