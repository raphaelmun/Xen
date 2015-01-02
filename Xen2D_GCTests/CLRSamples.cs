using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    //This class contains samples and tests for understanding of how the CLR works
    [TestClass]
    public class CLRSamples
    {
        //Calling SomeMethod( 1 ) would seem to be ambiguous between the following two overloads of SomeMethod, but it compiles and 
        //calls the explicit version.  
        private void SomeMethod( int i ) { }

        //Using the "params" keyword generates garbage because it creates an array, which is a reference type.  
        private void SomeMethod( params int[] args )
        {
            int length = args.Length;
        }

        [TestMethod]
        public void ParamArgumentsGeneratesGarbage()
        {
            HeapUtility.GetHeapStateBefore();
            SomeMethod( 1, 2, 3, 4 );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreNotEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void DeclaringArrayAllocatesFromHeap()
        {
            //All arrays inherit from System.Array, which is a reference type
            HeapUtility.GetHeapStateBefore();
            int[] arrayInts = new int[ 0 ]; //this generates the newarr instruction in ildasm, which allocates from heap
            HeapState delta = HeapUtility.GetHeapStateDelta();

            Assert.AreNotEqual( HeapState.Neutral, delta );
        }

        interface ITestInterface
        {
            int Value { get; set; }
        }
        struct TestStruct : ITestInterface
        {
            public int Value { get; set; }
        }

        [TestMethod]
        public void CastingStructToInterfaceCausesBoxing()
        {
            TestStruct ts = new TestStruct();
            ts.Value = 5;

            HeapUtility.GetHeapStateBefore();
            int i = ( (ITestInterface)ts ).Value;

            //box        Xen2D_UnitTests.CLRSamples/TestStruct
            //callvirt   instance int32 Xen2D_UnitTests.CLRSamples/ITestInterface::get_Value()

            HeapState delta = HeapUtility.GetHeapStateDelta();

            //ISSUE: delta will show a neutral heapstate, why doesn't GetTotalMemory show the allocation from the box instruction?
        }

        [TestMethod]
        public void WeakReferenceShouldBeDeadAfterGC()
        {
            WeakReference wr = new WeakReference( new object() );
            Assert.IsTrue( wr.IsAlive );

            HeapUtility.CollectGarbage();

            Assert.IsFalse( wr.IsAlive );
        }

        [TestMethod]
        public void FirstCallToVector2DotZeroGeneratesGarbage()
        {
            Vector2 vec;
            HeapUtility.GetHeapStateBefore();
            vec = Vector2.Zero;
            HeapState delta = HeapUtility.GetHeapStateDelta();

            Assert.AreNotEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void SecondCallToVector2DotZeroDoesNotGeneratesGarbage()
        {
            Vector2 vec = Vector2.Zero;
            HeapUtility.GetHeapStateBefore();
            vec = Vector2.Zero;
            HeapState delta = HeapUtility.GetHeapStateDelta();

            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
