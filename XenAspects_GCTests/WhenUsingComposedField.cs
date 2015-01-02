using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_GCTests
{
    public class MockFieldType : ComposableObject<MockFieldType>
    {
        public bool AcquireCalled = false;
        public MockFieldType CustomAcqiure()
        {
            AcquireCalled = true;
            return _pool.Acquire();
        }

        public bool ResetCalled = false;
        public override void Reset()
        {
            ResetCalled = true;
            base.Reset();
        }

        public bool ReleaseCalled = false;
        protected override void ReleaseInternal()
        {
            ReleaseCalled = true;
            base.ReleaseInternal();
        }
    }

    [TestClass]
    public class WhenUsingComposedField
    {
        [TestMethod]
        public void AcquireFieldDoesNotGenerateGarbage()
        {
            MockFieldType mf = new MockFieldType();
            MockFieldType.Pool.InitPool( 1 );
            ComposedField<MockFieldType> cf = new ComposedField<MockFieldType>( mf.CustomAcqiure );

            HeapUtility.GetHeapStateBefore();

            cf.AcquireField();

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
