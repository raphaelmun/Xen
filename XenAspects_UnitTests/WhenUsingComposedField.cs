using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
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
        public void ConstructingNewComposedFieldDoesNotAcquireFieldType()
        {
            MockFieldType mf = new MockFieldType();
            Assert.IsFalse( mf.AcquireCalled );
            ComposedField<MockFieldType> cf = new ComposedField<MockFieldType>( mf.CustomAcqiure );

            Assert.IsNull( cf.Value );
            Assert.IsFalse( mf.AcquireCalled );
        }

        [TestMethod]
        public void AcquireFieldCallsAcquireDelegate()
        {
            MockFieldType mf = new MockFieldType();
            ComposedField<MockFieldType> cf = new ComposedField<MockFieldType>( mf.CustomAcqiure );
            cf.AcquireField();

            Assert.IsNotNull( cf.Value );
            Assert.IsTrue( cf.Value.IsAcquired );
            Assert.IsTrue( mf.AcquireCalled );
        }

        [TestMethod]
        public void AcquiringFieldCallsReset()
        {
            MockFieldType mf = new MockFieldType();
            ComposedField<MockFieldType> cf = new ComposedField<MockFieldType>( mf.CustomAcqiure );
            cf.AcquireField();

            Assert.IsTrue( cf.Value.ResetCalled );
        }

        [TestMethod]
        public void ReleaseFieldWorks()
        {
            MockFieldType mf = new MockFieldType();
            ComposedField<MockFieldType> cf = new ComposedField<MockFieldType>( mf.CustomAcqiure );
            cf.AcquireField();

            MockFieldType actualField = cf.Value;
            Assert.IsFalse( actualField.ReleaseCalled );

            cf.ReleaseField();

            Assert.IsTrue( actualField.IsAvailable );
            Assert.IsTrue( actualField.ReleaseCalled );
            Assert.IsNull( cf.Value );
        }
    }
}
